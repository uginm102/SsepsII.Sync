using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Synchronisation.DAL;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Transactions;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;

namespace SsepsII.Synchronisation.Services
{
    public class SyncServices
    {
        #region --- Sync only ---

        private SyncServiceReference.SynchronizationWebServiceSoapClient syncProxy = new SyncServiceReference.SynchronizationWebServiceSoapClient();
        private SyncServiceReference.SecuredWebServiceHeader _tokenHeader = new SyncServiceReference.SecuredWebServiceHeader();

        public bool ServerIsAvailable(SystemUser user)
        {
            return LoginRemoteService(user);
        }

        /// <summary>
        /// This methode will not login on the server because it will try to use the servers credentials
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool LoginRemoteService(SystemUser user)
        {
            SyncServiceReference.SecuredWebServiceHeader header = new SyncServiceReference.SecuredWebServiceHeader();
            header.SiteID = SiteConfigServices.SiteID.ToString();
            header.InstallationToken = SiteConfigServices.InstallationToken;

            try
            {
                string feedback = syncProxy.AuthenticateUser(header);
                //TODO clean up LogServices.Log("SyncServices", "LoginRemoteService", feedback, user);
                XDocument xDoc = XDocument.Parse(feedback);


                if (!(bool)xDoc.Element("SecuredWebService").Attribute("success")) return false;

                else
                {
                    _tokenHeader.AuthenticatedToken = xDoc.Element("SecuredWebService").Element("token").Value;
                    return true;
                }
            }
            catch (EndpointNotFoundException ex)
            {
                //TODO log this error LogServices.Log("SyncServices", "LoginRemoteService", ex.Message, user,2,ex.InnerException.Message);
                return false;
            }

        }

        public bool SendSyncDataThroughLAN(SystemUser user)
        {
            if (LoginRemoteService(user))
            {
                //XDocument doc = GetDataToSyncSecure(user);
                string feedback = syncProxy.ReceiveSyncTransaDataObjects(_tokenHeader, GetDataToSyncSecure(user), SiteConfigServices.SiteID);
                XDocument xDoc = XDocument.Parse(feedback);
                return (bool)xDoc.Element("SecuredWebService").Attribute("success");
            }
            return false;
        }

        public bool ReceiveSyncDataTroughLAN(SystemUser user)
        {
            if (LoginRemoteService(user))
            {
                string feedback = syncProxy.SendApprovedData(_tokenHeader, SiteConfigServices.SiteID);
                XDocument xDoc = XDocument.Parse(Crypto.DecryptStringAES(feedback, SiteConfigServices.InstallationToken));
                if ((bool)xDoc.Element("SecuredWebService").Attribute("success"))
                {
                    //TODO using .Value throws "Data at the root level is invalid. Line 1, position 1." exception
                    //investigate whether the server is using wrong encoding which can be a pain in the ass in future.
                    //However it seems using .ToString() solves it.
                    //string test = xDoc.Element("SecuredWebService").ToString();
                    //string test1 = xDoc.Element("SecuredWebService").Value;

                    XDocument xDocData = XDocument.Parse(xDoc.Element("SecuredWebService").Element("data").Element("Approved").ToString());
                    ImportApprovedData(xDocData);
                    return true;
                }
            }
            return false;
        }

        public bool ImportDataForApproval(SystemUser user, string data, int siteId)
        {
            if (LoginRemoteService(user))
            {
                //XDocument doc = GetDataToSyncSecure(user);
                string feedback = syncProxy.ReceiveSyncTransaDataObjects(_tokenHeader, data, siteId);
                XDocument xDoc = XDocument.Parse(feedback);
                return (bool)xDoc.Element("SecuredWebService").Attribute("success");
            }
            return false;
        }

        public string GetApprovedDataToSyncSecure(List<int> mdaIdList, SystemUser user)
        {
            if (mdaIdList.Count == 0) return string.Empty;

            //TODO check that all these mda's are from the same installation
            if (LoginRemoteService(user))
            {
                InstallationServices installationServices = new InstallationServices();
                int siteId = installationServices.GetSiteIdFromMDAIdOnServer(mdaIdList.First());
                string feedback = syncProxy.SendApprovedData(_tokenHeader, siteId);
                XDocument xDoc = XDocument.Parse(Crypto.DecryptStringAES(feedback, installationServices.InstallationTokenBySiteId(siteId)));
                if ((bool)xDoc.Element("SecuredWebService").Attribute("success"))
                {
                    //TODO using .Value throws "Data at the root level is invalid. Line 1, position 1." exception
                    //investigate whether the server is using wrong encoding which can be a pain in the ass in future.
                    //However it seems using .ToString() solves it.
                    //string test = xDoc.Element("SecuredWebService").ToString();
                    //string test1 = xDoc.Element("SecuredWebService").Value;

                    XDocument xDocData = XDocument.Parse(xDoc.Element("SecuredWebService").Element("data").Element("Approved").ToString());
                    ImportApprovedData(xDocData);
                    return Crypto.EncryptStringAES(xDocData.ToString(), installationServices.InstallationTokenByMDAId(mdaIdList.First()));
                }
            }
            return string.Empty;
        }

        #endregion

        #region --- shared ---

        public List<TransData> GetDataToSync(SystemUser user)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                //Mark them as exported
                foreach (TransData row in ents.TransDatas.Where(x => user.AssignedMdaIdList.Contains(x.MdaId) && x.state == (int)SyncState.Normal).ToList())
                {
                    row.state = (int)SyncState.Exported;
                }

                ents.SaveChanges();

                return ents.TransDatas.Where(x => user.AssignedMdaIdList.Contains(x.MdaId) &&
                    (x.state == (int)SyncState.Normal || x.state == (int)SyncState.Exported || x.state == (int)SyncState.ConsolidateOnly)).OrderBy(o => o.MdaId).ToList();

            }
        }

        public string GetDataToSyncSecure(SystemUser user)
        {
            return Crypto.EncryptStringAES(GetDataToSyncXML(user).ToString(), SiteConfigServices.InstallationToken);
        }

        public XDocument GetDataToSyncXML(SystemUser user)
        {
            List<TransData> dataToSync = GetDataToSync(user);
            XDocument xDoc = new XDocument(new XDeclaration("1.0", "UTF-16", null), new XElement("Sync"));

            foreach (TransData data in dataToSync)
            {
                if (data.state == (int)SyncState.ConsolidateOnly)
                {
                    GetConsolidationData(xDoc, data);
                }
                else
                {
                    GetSyncData(xDoc, data);
                }
            }

            xDoc.Element("Sync").Add(GetEmployeesToTransferToServer());
            return xDoc;
        }

        private void GetSyncData(XDocument xDoc, TransData data)
        {
            if (!xDoc.Element("Sync").Elements("sync-data").Any()) xDoc.Element("Sync").Add(new XElement("sync-data"));

            if (xDoc.Element("Sync").Elements("sync-data").Elements("MDA").Where(x => x.Attribute("Id").Value == data.MdaId.ToString()).Count() == 1)
            {
                XElement mdaXElement = xDoc.Element("Sync").Elements("sync-data").Elements("MDA").FirstOrDefault(x => x.Attribute("Id").Value == data.MdaId.ToString());

                if (mdaXElement != null)
                {
                    //XElement rows;
                    //if (mdaXElement.Elements("Rows").Any())
                    //    rows = mdaXElement.Element("Rows");
                    //else mdaXElement.Add(rows = new XElement("Rows"));
                    mdaXElement.Add(GetSyncDataRow(data));
                }
                else
                {
                    //We don't expect this to ever be null
                    new MissingMemberException("Sync mda node");
                }
            }
            else
            {
                //TODO we assume sync data will always be single
                xDoc.Element("Sync").Element("MDAs").Element("sync-data").Add(
                    new XElement("MDA", new XAttribute("Id", data.MdaId),
                        new XElement("Period", data.SyncPeriod),
                            new XElement("Rows", GetSyncDataRow(data))));
            }
        }

        private static XElement GetSyncDataRow(TransData data)
        {
            return new XElement("Row",
                new XElement("Guid", data.RowGuid),
                new XElement("ActionType", data.ActionType),
                new XElement("TableName", data.TableName),
                new XElement("TransDate", data.TransDate),
                new XElement("OldValues", data.OldValues),
                new XElement("NewValues", data.NewValues),
                new XElement("EmployeeID", data.EmployeeID),
                new XElement("SyncPeriod", data.SyncPeriod),
                new XElement("LogRefID", data.LogRefID),
                new XElement("Username", data.Username),
                new XElement("state", data.state)
                );
        }

        private void GetConsolidationData(XDocument xDoc, TransData data)
        {

            if (!xDoc.Element("Sync").Elements("Consolidation-data").Any()) xDoc.Element("Sync").Add(new XElement("Consolidation-data"));
            if (xDoc.Element("Sync").Element("Consolidation-data").Elements("MDA").Where(x => x.Attribute("Id").Value == data.MdaId.ToString()).Count() == 0)
            {
                xDoc.Element("Sync").Element("Consolidation-data").Add(new XElement("MDA", new XAttribute("Id", data.MdaId)));
            }

            //PayrollHistory
            if (data.TableName == Constant.EMPLOYEE_PAYROLLHISTORY_TABLE)
            {
                GetEmployeePayrollHistoryData(xDoc, data);
            }
        }

        private void GetEmployeePayrollHistoryData(XDocument xDoc, TransData data)
        {
            XElement records;
            if (xDoc.Element("Sync").Element("Consolidation-data").Elements("MDA").Elements(Constant.EMPLOYEE_PAYROLLHISTORY_TABLE)
                .Where(x => x.Attribute("SyncPeriod").Value == data.SyncPeriod.ToString()).Count() > 0) new ArgumentException(string.Format("Duplicate payroll periods for mda {0}", data.MdaId));

            XElement payrollHistoryXElement = xDoc.Element("Sync").Element("Consolidation-data").Elements("MDA").FirstOrDefault(x => x.Attribute("Id").Value == data.MdaId.ToString());
            payrollHistoryXElement.Add(records = new XElement(Constant.EMPLOYEE_PAYROLLHISTORY_TABLE, new XAttribute("SyncPeriod", data.SyncPeriod)));
            if (payrollHistoryXElement != null)
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    foreach (EmployeePayrollHistory empPH in ents.EmployeePayrollHistories.Where(x => x.mdaID == data.MdaId && x.payrollPeriod == data.SyncPeriod).ToList())
                    {
                        records.Add(new XElement(empPH.AsXmlRow));
                    }
                }
            }
            else
            {
                //We don't expect this to ever be null
                new MissingMemberException("Sync mda node");
            }
        }

        public void ImportApprovedData(XDocument xDoc)
        {
            List<string> pkApprovedData = new List<string>();
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    List<TransData> dataToSync = new List<TransData>();
                    foreach (XElement rowElement in xDoc.Element("Approved").Element("TransData").Elements("Row").ToList())
                    {
                        string logRefID = string.Empty;
                        switch ((int)rowElement.Element("state"))
                        {
                            case (int)SyncState.Rejected:
                                if (rowElement.Element("LogRefID") == null) new ArgumentNullException("LogRefID");
                                logRefID = (string)rowElement.Element("LogRefID");
                                TransData approvalStatusRejected = ents.TransDatas.Where(o => o.LogRefID == logRefID).FirstOrDefault();
                                if (approvalStatusRejected == null) new InvalidDataException("Sync Row not found on client machine");
                                //If it was rejected and not edited further on the client machine, then reject.
                                //However, if it was edited by the user then assume new changes to be valid ones.
                                if (approvalStatusRejected.state == (int)SyncState.Exported)
                                {
                                    approvalStatusRejected.state = (int)SyncState.Rejected;
                                    pkApprovedData.Add(logRefID);
                                }
                                break;
                            case (int)SyncState.Approved:
                                if (rowElement.Element("LogRefID") == null) new ArgumentNullException("LogRefID");
                                logRefID = (string)rowElement.Element("LogRefID");
                                TransData approvalStatusAccepted = ents.TransDatas.Where(o => o.LogRefID == logRefID).FirstOrDefault();
                                if (approvalStatusAccepted == null) new InvalidDataException("Sync Row not found on client machine");
                                approvalStatusAccepted.state = (int)SyncState.Approved;
                                pkApprovedData.Add(logRefID);
                                approvalStatusAccepted.NewValues = rowElement.Element("newValue").ToString();
                                break;
                            case (int)SyncState.SyncedConsolidateOnly:
                                int mdaId = 0, syncPeriod = 0;
                                if (rowElement.Element("SyncPeriod") == null) new ArgumentNullException("SyncPeriod");
                                else syncPeriod = (int)rowElement.Element("SyncPeriod");
                                if (rowElement.Element("MdaId") == null) new ArgumentNullException("MdaId");
                                else mdaId = (int)rowElement.Element("MdaId");
                                TransData approvalStatusConsolidated = ents.TransDatas.Where(o => o.MdaId == mdaId && o.SyncPeriod == syncPeriod).FirstOrDefault();
                                approvalStatusConsolidated.state = (int)SyncState.SyncedConsolidateOnly;
                                break;
                        }
                        ents.SaveChanges();
                    }

                    foreach (XElement rowElement in xDoc.Element("Approved").Element("Transfer-status").Elements("Row").ToList())
                    {
                        long employeeId = (long)rowElement.Element("EmployeeId");
                        TransferredEmployee transferredEmployee = ents.TransferredEmployees.Find(employeeId);
                        if (transferredEmployee != null)
                        {
                            transferredEmployee.TransferState = (int)rowElement.Element("TransferState");
                            ents.SaveChanges();
                        }
                    }

                    List<long> empIds = ents.TransDatas.Where(x => pkApprovedData.Contains(x.LogRefID)).Select(o => o.EmployeeID).Distinct().ToList();
                    foreach (long empId in empIds)
                    {
                        EffectApprovalsByEmployeeID(empId);
                    }

                    #region Transfers
                    ConsolidateTransfers(xDoc.Element("Approved").Element("Transfers"));
                    #endregion
                    scope.Complete();
                }
            }
        }

        public void EffectApprovalsByEmployeeID(long empId)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                SyncData syncData = SyncFactory.GetSyncObject(ents.TransDatas.Where(x => x.EmployeeID == empId).ToList());
                syncData.Save(SyncType.Approval);
            }
        }

        public void ConsolidateTransfers(XElement records)
        {
            //TODO clean up.
            if (records == null) return;
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                List<TransferredEmployee> transferedEmployees = new List<TransferredEmployee>();
                TransferredEmployee tempTransferedEmployee;
                foreach (XElement transferedEmpElement in records.Elements("Transfer").ToList())
                {
                    tempTransferedEmployee = new TransferredEmployee()
                    {
                        EmployeeID = long.Parse(transferedEmpElement.Element("EmployeeID").Value),
                        TransferReason = transferedEmpElement.Element("TransferReason").Value,
                        TransferXML = transferedEmpElement.Element("TransferXML").Value,
                        TransferDirection = transferedEmpElement.Element("TransferDirection").Value,
                        FromDirectorateId = int.Parse(transferedEmpElement.Element("FromDirectorateId").Value),
                        FromGrade = int.Parse(transferedEmpElement.Element("FromGrade").Value),
                        StartPeriodDestination = int.Parse(transferedEmpElement.Element("StartPeriodDestination").Value),
                        TransferState = int.Parse(transferedEmpElement.Element("TransferState").Value),
                        ToGrade = int.Parse(transferedEmpElement.Element("ToGrade").Value),
                        ToMDAID = int.Parse(transferedEmpElement.Element("ToMDAID").Value),
                        FromMDAID = int.Parse(transferedEmpElement.Element("FromMDAID").Value),
                        AuthorizedBy = transferedEmpElement.Element("AuthorizedBy").Value,
                        AuthorizedOn = DateTime.Parse(transferedEmpElement.Element("AuthorizedOn").Value),
                        dateCreated = DateTime.Parse(transferedEmpElement.Element("dateCreated").Value),
                        whoCreated = transferedEmpElement.Element("whoCreated").Value
                    };
                    if (transferedEmpElement.Element("dateUpdated") != null) tempTransferedEmployee.dateUpdated = DateTime.Parse(transferedEmpElement.Element("dateUpdated").Value);

                    //TODO why is this varbinary?
                    //if (transferedEmpElement.Element("whoCreated") != null) tempTransferedEmployee.whoUpdated = transferedEmpElement.Element("TransferReason").Value
                    transferedEmployees.Add(tempTransferedEmployee);
                }

                //SqlConnection dbConn = ents.Database.Connection as SqlConnection;
                //dbConn.Open();
                //SqlBulkCopy bulkInsert = new SqlBulkCopy(dbConn);
                //bulkInsert.DestinationTableName = "TransferredEmployee";
                //bulkInsert.WriteToServer(transferedEmployees.AsDataReader());
                //dbConn.Close();
            }
        }

        /// <summary>
        /// Get's all pending transfers that need to be synced with the server for all mda's on this site
        /// </summary>
        /// <returns></returns>
        public XElement GetEmployeesToTransferToServer()
        {
            XElement transfers = new XElement("Transfers");
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                int sourceCreatedTransfer = (int)TransferState.SourceCreatedTransfer;
                foreach (TransferredEmployee transferredEmp in ents.TransferredEmployees.Where(x => x.TransferState == sourceCreatedTransfer).ToList())
                {

                    XElement transfer = new XElement("Transfer",
                        new XElement("EmployeeID", transferredEmp.EmployeeID),
                        new XElement("TransferReason", transferredEmp.TransferReason),
                        new XElement("TransferXML", transferredEmp.TransferXML),
                        new XElement("TransferDirection", transferredEmp.TransferDirection),
                        new XElement("FromDirectorateId", transferredEmp.FromDirectorateId),
                        new XElement("FromGrade", transferredEmp.FromGrade),
                        new XElement("StartPeriodDestination", transferredEmp.StartPeriodDestination),
                        new XElement("TransferState", transferredEmp.TransferState),
                        new XElement("ToGrade", transferredEmp.ToGrade),
                        new XElement("ToMDAID", transferredEmp.ToMDAID),
                        new XElement("FromMDAID", transferredEmp.FromMDAID),
                        new XElement("AuthorizedBy", transferredEmp.AuthorizedBy),
                        new XElement("AuthorizedOn", transferredEmp.AuthorizedOn),
                        new XElement("dateCreated", transferredEmp.dateCreated),
                        new XElement("whoCreated", transferredEmp.whoCreated));
                    if (transferredEmp.dateUpdated.HasValue) transfer.Add(new XElement("dateUpdated", transferredEmp.dateUpdated));
                    //transfers.Add(transfer);
                }
            }
            return transfers;
        }

        #endregion
    }
}