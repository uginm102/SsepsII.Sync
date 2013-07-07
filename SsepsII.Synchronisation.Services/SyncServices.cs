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
        
        private SyncServiceReference.SynchronizationWebServiceSoapClient syncProxy = new SyncServiceReference.SynchronizationWebServiceSoapClient();
        private SyncServiceReference.SecuredWebServiceHeader _tokenHeader = new SyncServiceReference.SecuredWebServiceHeader();
        
        public bool LoginRemoteService(SystemUser user)
        {
            SyncServiceReference.SecuredWebServiceHeader header = new SyncServiceReference.SecuredWebServiceHeader();
            header.SiteID = SiteConfigServices.SiteID.ToString();
            header.InstallationToken = SiteConfigServices.InstallationToken;

            try
            {
                string feedback = syncProxy.AuthenticateUser(header);
                LogServices.Log("SyncServices", "LoginRemoteService", feedback, user);
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
                LogServices.Log("SyncServices", "LoginRemoteService", ex.Message, user,2,ex.InnerException.Message);
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

        public List<TransData> GetDataToSync(SystemUser user)
        {
            int normal = (int)SyncState.Normal;
            int exported = (int)SyncState.Exported;
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                //Mark them as exported
                foreach (TransData row in ents.TransDatas.Where(x => user.AssignedMdaIdList.Contains(x.MdaId) && x.state == normal).ToList())
                {
                    row.state = (int)SyncState.Exported;
                }

                ents.SaveChanges();

                return ents.TransDatas.Where(x => user.AssignedMdaIdList.Contains(x.MdaId) && (x.state == normal || x.state == exported)).OrderBy(o => o.MdaId).ToList();
            }
        }

        public List<ApprovalRowBO> GetApprovedDataToSync(List<int> mdaIdList)
        {
            int normal = (int)SyncState.Normal;
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                List<ApprovalRowBO> approved = ents.TransDatas.Where(x => mdaIdList.Contains(x.MdaId) && x.state != normal).Select(x => new ApprovalRowBO() { LogRefID = x.LogRefID, MdaId = x.MdaId, State = x.state, NewValue = x.NewValues }).OrderBy(o => o.MdaId).ToList();
                return approved;
            }
        }

        public string GetDataToSyncSecure(SystemUser user)
        {
            return string.Format("{0}|{1}",SiteConfigServices.SiteID, Crypto.EncryptStringAES(GetDataToSyncXML(user).ToString(), SiteConfigServices.InstallationToken));
        }

        public string GetApprovedDataToSyncSecure(List<int> mdaIdList)
        {
            if (mdaIdList.Count > 0)
            {
                int mdaId = mdaIdList.First();
                InstallationServices installationServices = new InstallationServices();

                return Crypto.EncryptStringAES(GetApprovedDataToSyncXML(mdaIdList).ToString(), installationServices.InstallationTokenByMDAId(mdaId));
            }
            else return string.Empty;
        }

        public string GetApprovedDataToSyncSecure(List<int> mdaIdList, int siteId)
        {
            InstallationServices installationServices = new InstallationServices();
            return Crypto.EncryptStringAES(GetApprovedDataToSyncXML(mdaIdList).ToString(), installationServices.InstallationTokenBySiteId(siteId));
        }

        public XDocument GetDataToSyncXML(SystemUser user)
        {
            List<TransData> dataToSync = GetDataToSync(user);
            XDocument xDoc = new XDocument(
            new XDeclaration("1.0", "UTF-16", null),
            new XElement("Sync",
                new XElement("MDAs")
                    ));

            XElement rows = null;
            //Add data per mda
            int mdaId = 0;

            foreach (TransData data in dataToSync)
            {
                //New mda
                if (mdaId == 0 || mdaId != data.MdaId)
                {
                    mdaId = data.MdaId;
                    xDoc.Element("Sync").Element("MDAs").Add(
                         new XElement("MDA",
                            new XElement("Id", data.MdaId),
                            new XElement("Period", data.SyncPeriod),
                            rows = new XElement("Rows",
                                new XElement("Row",
                                    new XElement("Guid", data.RowGuid),
                                    new XElement("ActionType", data.ActionType),
                                    new XElement("TableName", data.TableName),
                                    new XElement("TransDate", data.TransDate),
                                    new XElement("OldValues", data.OldValues),
                                    new XElement("NewValues", data.NewValues),
                                    new XElement("EmployeeID", data.EmployeeID),
                                    new XElement("LogRefID", data.LogRefID),
                                    new XElement("Username", data.Username),
                                    new XElement("state", data.state))
                                    )));
                }
                //Same mda
                else if (mdaId == data.MdaId)
                {
                    rows.Add(
                        new XElement("Row",
                                    new XElement("Guid", data.RowGuid),
                                    new XElement("ActionType", data.ActionType),
                                    new XElement("TableName", data.TableName),
                                    new XElement("TransDate", data.TransDate),
                                    new XElement("OldValues", data.OldValues),
                                    new XElement("NewValues", data.NewValues),
                                    new XElement("EmployeeID", data.EmployeeID),
                                    new XElement("LogRefID", data.LogRefID),
                                    new XElement("Username", data.Username),
                                    new XElement("state", data.state)
                                    ));
                }
            }

            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                XElement records = null;

                int consolidateOnly = (int)SyncState.ConsolidateOnly;
                foreach (TransData table in ents.TransDatas.Where(x => x.state == consolidateOnly).ToList())
                {
                    xDoc.Element("Sync").Element("MDAs").Add(
                        new XElement("Id", table.MdaId),
                        new XElement("Period", table.SyncPeriod),
                        new XElement("TableName", table.TableName),
                        new XElement("Username", table.Username),
                        records = new XElement("Records")
                        );
                    foreach (EmployeePayrollHistory empPH in ents.EmployeePayrollHistories.Where(x => x.mdaID == table.MdaId && x.payrollPeriod == table.SyncPeriod).ToList())
                    {
                        records.Add(new XElement(empPH.AsXmlRow));
                    }
                }
            }

            return xDoc;
        }

        public XDocument GetApprovedDataToSyncXML(List<int> mdaIdList)
        {
            List<ApprovalRowBO> approvedDataToSync = GetApprovedDataToSync(mdaIdList);
            XElement rows;
            XDocument xDoc = new XDocument(
            new XDeclaration("1.0", "UTF-16", null),
            new XElement("Approved",
                rows = new XElement("Rows")
                    ));


            foreach (ApprovalRowBO data in approvedDataToSync)
            {
                rows.Add(data.AsXmlRow);
            }
            return xDoc;
        }

        public void ImportApprovedData(XDocument xDoc)
        {
            List<string> pkApprovedData = new List<string>();
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    List<TransData> dataToSync = new List<TransData>();
                    foreach (XElement rowElement in xDoc.Element("Approved").Element("Rows").Elements("Row").ToList())
                    {
                        int mdaId = (int)rowElement.Element("MdaId");
                        string logRefID = rowElement.Element("LogRefID") != null ? (string)rowElement.Element("LogRefID") : string.Empty;
                        if (logRefID != string.Empty)
                        {
                            TransData approvalStatus = ents.TransDatas.Where(o => o.MdaId == mdaId && o.LogRefID == logRefID).FirstOrDefault();
                            if (approvalStatus != null)
                            {
                                //If it was approved, over write any new changes
                                if ((int)rowElement.Element("state") == (int)SyncState.Approved)
                                {
                                    pkApprovedData.Add(logRefID);
                                    approvalStatus.state = (int)rowElement.Element("state");
                                    approvalStatus.NewValues = rowElement.Element("newValue").FirstNode.ToString();
                                }
                                else if (approvalStatus.state == (int)SyncState.Exported)//If state didn't change at all, then we implement the state from server
                                {
                                    pkApprovedData.Add(logRefID);
                                    approvalStatus.state = (int)rowElement.Element("state");
                                }//If any changes were made, then we leave it as it is. 
                                //TODO probably best to infor the user of such changes
                                ents.SaveChanges();
                            }
                        }
                        else
                        {
                            //TODO This is a consolidation data. Usually it will be one per mda till further notice
                            ents.Database.ExecuteSqlCommand(string.Format("UPDATE dbo.TransData SET state = 4 WHERE MdaId = {0}", mdaId));
                        }
                    }

                    //TODO what happens if this is called on server and yet we have some data that needs to stay here till it's exported
                    //However, less likely to import approved data on a server
                    ents.Database.ExecuteSqlCommand(string.Format("DELETE FROM dbo.TransData WHERE state = 4"));
                    List<long> empIds = ents.TransDatas.Where(x => pkApprovedData.Contains(x.LogRefID)).Select(o => o.EmployeeID).Distinct().ToList();
                    foreach (long empId in empIds)
                    {
                        EffectApprovalsByEmployeeID(empId);
                    }
                    scope.Complete();
                }
            }
        }

        public void ImportDataForApproval(XDocument xDoc)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {

                using (TransactionScope scope = new TransactionScope())
                {

                    try
                    {
                        List<TransData> dataToSync = new List<TransData>();
                        foreach (XElement mdaElement in xDoc.Element("Sync").Element("MDAs").Elements("MDA").ToList())
                        {
                            //TODO this will mean that users have to first import approved data before they can export data again
                            ents.Database.ExecuteSqlCommand("DELETE FROM dbo.TransData WHERE MdaId = {0}", (int)mdaElement.Element("Id"));
                            foreach (XElement rowElement in mdaElement.Element("Rows").Elements("Row").ToList())
                            {
                                dataToSync.Add(new TransData()
                                {
                                    MdaId = (int)mdaElement.Element("Id"),
                                    SyncPeriod = (int)mdaElement.Element("Period"),
                                    RowGuid = (int)rowElement.Element("Guid"),
                                    ActionType = (string)rowElement.Element("ActionType"),
                                    TableName = (string)rowElement.Element("TableName"),
                                    TransDate = (DateTime)rowElement.Element("TransDate"),
                                    OldValues = (string)rowElement.Element("OldValues"),
                                    NewValues = (string)rowElement.Element("NewValues"),
                                    EmployeeID = (long)rowElement.Element("EmployeeID"),
                                    LogRefID = (string)rowElement.Element("LogRefID"),
                                    Username = (string)rowElement.Element("Username"),
                                    state = (int)rowElement.Element("state")
                                });
                            }
                        }
                        List<int> payrollHistoryConsolidatedMDAS = new List<int>();
                        foreach (XElement mdaElement in xDoc.Element("Sync").Element("MDAs").Elements("ConsolidationData").ToList())
                        {
                            ents.Database.ExecuteSqlCommand("DELETE FROM dbo.TransData WHERE MdaId = {0}", (int)mdaElement.Element("Id"));
                            payrollHistoryConsolidatedMDAS.Add((int)mdaElement.Element("Id"));
                            dataToSync.Add(new TransData()
                            {
                                MdaId = (int)mdaElement.Element("Id"),
                                SyncPeriod = (int)mdaElement.Element("Period"),
                                ActionType = "Insert",
                                TableName = (string)mdaElement.Element("TableName"),
                                TransDate = DateTime.Now,
                                NewValues = string.Empty,
                                EmployeeID = 0,
                                LogRefID = string.Empty,
                                Username = (string)mdaElement.Element("Username"),
                                state = 3
                            });

                            ConsolidatePayrollHistory(mdaElement.Element("Records"));
                        }

                        SqlConnection dbConn = ents.Database.Connection as SqlConnection;
                        dbConn.Open();
                        SqlBulkCopy bulkInsert = new SqlBulkCopy(dbConn);
                        bulkInsert.DestinationTableName = "TransData";
                        bulkInsert.WriteToServer(dataToSync.AsDataReader());

                        StringBuilder sb = new StringBuilder();
                        if (payrollHistoryConsolidatedMDAS.Count > 0)
                        {
                            sb.Append("UPDATE dbo.TransData SET state = 4 WHERE STATE = 3 AND MdaId IN (");
                            foreach (int mdaId in payrollHistoryConsolidatedMDAS)
                            {
                                sb.Append(string.Format(" {0} ,", mdaId));
                            }
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append(" )");
                            ents.Database.ExecuteSqlCommand(sb.ToString());
                        }

                        dbConn.Close();
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        throw ex;
                    }
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

        public void ConsolidatePayrollHistory(XElement records)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                //get all the data
                List<EmployeePayrollHistory> payrollHistories = new List<EmployeePayrollHistory>();
                foreach (XElement empPHElement in records.Elements("EmployeePayrollHistory").ToList())
                {
                    var one = empPHElement.Element("EmployeeID");
                    var two = empPHElement.Element("payItemsXML").Element("PayItems").ToString();
                    payrollHistories.Add(new EmployeePayrollHistory()
                    {

                        EmployeeID = long.Parse(empPHElement.Element("EmployeeID").Value),
                        payrollPeriod = int.Parse(empPHElement.Element("payrollPeriod").Value),
                        mdaID = int.Parse(empPHElement.Element("mdaID").Value),
                        directorateID = int.Parse(empPHElement.Element("directorateID").Value),
                        payrollDate = DateTime.Parse(empPHElement.Element("payrollDate").Value),
                        payItemsXML = empPHElement.Element("payItemsXML").Element("PayItems").ToString(),
                        grossPay = decimal.Parse(empPHElement.Element("grossPay").Value),
                        netPay = decimal.Parse(empPHElement.Element("netPay").Value),
                        payrollPreparedBy = empPHElement.Element("payrollPreparedBy").Value,
                        payrollVerifiedBy = empPHElement.Element("payrollVerifiedBy").Value,
                        payrollApprovedBy = empPHElement.Element("payrollApprovedBy").Value,
                        dateCreated = DateTime.Parse(empPHElement.Element("dateCreated").Value),
                        datePosted = DateTime.Parse(empPHElement.Element("datePosted").Value),
                        whoCreated = empPHElement.Element("whoCreated").Value,
                        whoPosted = empPHElement.Element("whoPosted").Value,
                        LogRefID = empPHElement.Element("LogRefID") != null ? empPHElement.Element("LogRefID").Value : null
                    });
                }

                SqlConnection dbConn = ents.Database.Connection as SqlConnection;
                dbConn.Open();
                SqlBulkCopy bulkInsert = new SqlBulkCopy(dbConn);
                bulkInsert.DestinationTableName = "EmployeePayrollHistory";
                bulkInsert.WriteToServer(payrollHistories.AsDataReader());
                dbConn.Close();
            }
        }
    }
}
