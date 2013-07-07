using SsepsII.Synchronisation.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Transactions;

namespace SsepsII.Synchronisation.Services
{
    public class SyncData
    {
        public List<TransData> Tables { get; set; }

        public int MdaId { get; set; }

        public long EmployeeId { get; set; }

        public SyncState State { get; set; }

        public SyncData(List<TransData> tables)
        {
            Tables = tables;
            MdaId = Tables.First().MdaId;
            EmployeeId = Tables.First().EmployeeID;
            State = (SyncState)Tables.First().state;
        }

        #region Table manipulations

        /// <summary>
        /// If the employee already exists, then do an update.
        /// </summary>
        public void InsertEmployee()
        {
            bool exists = false;
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                //Skip if table doesn't exist
                if (Tables.Where(x => x.TableName == Constant.EMPLOYEE_TABLE).Count() == 0) return;
                XmlAttributeCollection attributes = Tables.Single(x => x.TableName == Constant.EMPLOYEE_TABLE).GetNewAttributesFromXml();
                Employee emp;

                if (ents.Employees.Where(x => x.EmployeeID == EmployeeId).Count() > 0)
                {
                    emp = ents.Employees.Find(EmployeeId);
                    exists = true;
                }
                else
                {
                    emp = new Employee();
                    emp.EmployeeID = EmployeeId;
                }

                emp.prefix = attributes.GetNamedItem("prefix").Value;
                emp.surName = attributes.GetNamedItem("surName").Value;
                emp.givenName = attributes.GetNamedItem("givenName").Value;
                emp.otherNames = attributes.GetNamedItem("otherNames").Value;
                emp.gender = attributes.GetNamedItem("gender").Value;
                emp.dateofBirth = DateTime.Parse(attributes.GetNamedItem("dateofBirth").Value);
                if (attributes.GetNamedItem("maritalStatus") != null) emp.maritalStatus = int.Parse(attributes.GetNamedItem("maritalStatus").Value);
                if (attributes.GetNamedItem("EmployeePIN") != null) emp.EmployeePIN = attributes.GetNamedItem("EmployeePIN").Value;
                if (attributes.GetNamedItem("NationalID") != null) emp.NationalID = attributes.GetNamedItem("NationalID").Value;
                emp.Nationality = attributes.GetNamedItem("Nationality").Value;
                if (attributes.GetNamedItem("hrmisCode") != null) emp.hrmisCode = attributes.GetNamedItem("hrmisCode").Value;
                emp.dateofAppointment = DateTime.Parse(attributes.GetNamedItem("dateofAppointment").Value);
                emp.IsActive = int.Parse(attributes.GetNamedItem("IsActive").Value) == 1 ? true : false;
                if (attributes.GetNamedItem("email") != null) emp.email = attributes.GetNamedItem("email").Value;
                if (attributes.GetNamedItem("phoneMobile") != null) emp.phoneMobile = attributes.GetNamedItem("phoneMobile").Value;
                if (attributes.GetNamedItem("phoneMobile2") != null) emp.phoneMobile2 = attributes.GetNamedItem("phoneMobile2").Value;
                if (attributes.GetNamedItem("phoneMobile3") != null) emp.phoneMobile3 = attributes.GetNamedItem("phoneMobile3").Value;
                if (attributes.GetNamedItem("home_StateID") != null) emp.home_StateID = int.Parse(attributes.GetNamedItem("home_StateID").Value);
                if (attributes.GetNamedItem("address") != null) emp.address = attributes.GetNamedItem("address").Value;
                emp.dateCreated = DateTime.Parse(attributes.GetNamedItem("dateCreated").Value);
                if (attributes.GetNamedItem("dateUpdated") != null) emp.dateUpdated = DateTime.Parse(attributes.GetNamedItem("dateUpdated").Value);
                emp.whoCreated = attributes.GetNamedItem("whoCreated").Value;
                if (attributes.GetNamedItem("whoUpdated") != null) emp.whoUpdated = attributes.GetNamedItem("whoUpdated").Value;
                if (attributes.GetNamedItem("LogRefID") != null) emp.LogRefID = attributes.GetNamedItem("LogRefID").Value;
                if (attributes.GetNamedItem("lastPromotionDate") != null) emp.lastPromotionDate = DateTime.Parse(attributes.GetNamedItem("lastPromotionDate").Value);
                if (attributes.GetNamedItem("lastIncrementDate") != null) emp.lastIncrementDate = DateTime.Parse(attributes.GetNamedItem("lastIncrementDate").Value);

                ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeEventLog ON dbo.Employee");
                if (!exists) ents.Employees.Add(emp);
                ents.SaveChanges();
                ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeEventLog ON dbo.Employee");
            }
        }

        public void DeleteEmployee()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeEventLog ON dbo.Employee");
                ents.Database.ExecuteSqlCommand(string.Format("DELETE FROM dbo.Employee WHERE EmployeeID = {0}", EmployeeId));
                ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeEventLog ON dbo.Employee");
            }
        }

        /// <summary>
        /// If old value exists in transData, then revert to it otherwise delete the employee.
        /// </summary>
        public void UndoEmployee()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                if (Tables.Where(x => x.TableName == Constant.EMPLOYEE_TABLE).Count() == 0) return;
                TransData table = Tables.Single(x => x.TableName == Constant.EMPLOYEE_TABLE);

                if (table.HasOldValue())
                {
                    XmlAttributeCollection attributes = table.GetOldAttributesFromXml();
                    Employee emp = ents.Employees.Find(EmployeeId);

                    emp.prefix = attributes.GetNamedItem("prefix").Value;
                    emp.surName = attributes.GetNamedItem("surName").Value;
                    emp.givenName = attributes.GetNamedItem("givenName").Value;
                    emp.otherNames = attributes.GetNamedItem("otherNames").Value;
                    emp.gender = attributes.GetNamedItem("gender").Value;
                    emp.dateofBirth = DateTime.Parse(attributes.GetNamedItem("dateofBirth").Value);
                    if (attributes.GetNamedItem("maritalStatus") != null) emp.maritalStatus = int.Parse(attributes.GetNamedItem("maritalStatus").Value);
                    if (attributes.GetNamedItem("EmployeePIN") != null) emp.EmployeePIN = attributes.GetNamedItem("EmployeePIN").Value;
                    if (attributes.GetNamedItem("NationalID") != null) emp.NationalID = attributes.GetNamedItem("NationalID").Value;
                    emp.Nationality = attributes.GetNamedItem("Nationality").Value;
                    if (attributes.GetNamedItem("hrmisCode") != null) emp.hrmisCode = attributes.GetNamedItem("hrmisCode").Value;
                    emp.dateofAppointment = DateTime.Parse(attributes.GetNamedItem("dateofAppointment").Value);
                    emp.IsActive = int.Parse(attributes.GetNamedItem("IsActive").Value) == 1 ? true : false;
                    if (attributes.GetNamedItem("email") != null) emp.email = attributes.GetNamedItem("email").Value;
                    if (attributes.GetNamedItem("phoneMobile") != null) emp.phoneMobile = attributes.GetNamedItem("phoneMobile").Value;
                    if (attributes.GetNamedItem("phoneMobile2") != null) emp.phoneMobile2 = attributes.GetNamedItem("phoneMobile2").Value;
                    if (attributes.GetNamedItem("phoneMobile3") != null) emp.phoneMobile3 = attributes.GetNamedItem("phoneMobile3").Value;
                    if (attributes.GetNamedItem("home_StateID") != null) emp.home_StateID = int.Parse(attributes.GetNamedItem("home_StateID").Value);
                    if (attributes.GetNamedItem("address") != null) emp.address = attributes.GetNamedItem("address").Value;
                    emp.dateCreated = DateTime.Parse(attributes.GetNamedItem("dateCreated").Value);
                    if (attributes.GetNamedItem("dateUpdated") != null) emp.dateUpdated = DateTime.Parse(attributes.GetNamedItem("dateUpdated").Value);
                    emp.whoCreated = attributes.GetNamedItem("whoCreated").Value;
                    if (attributes.GetNamedItem("whoUpdated") != null) emp.whoUpdated = attributes.GetNamedItem("whoUpdated").Value;
                    if (attributes.GetNamedItem("LogRefID") != null) emp.LogRefID = attributes.GetNamedItem("LogRefID").Value;
                    if (attributes.GetNamedItem("lastPromotionDate") != null) emp.lastPromotionDate = DateTime.Parse(attributes.GetNamedItem("lastPromotionDate").Value);
                    if (attributes.GetNamedItem("lastIncrementDate") != null) emp.lastIncrementDate = DateTime.Parse(attributes.GetNamedItem("lastIncrementDate").Value);

                    ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeEventLog ON dbo.Employee");

                    ents.SaveChanges();

                    ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeEventLog ON dbo.Employee");
                }
                else
                {
                    DeleteEmployee();
                }
            }
        }

        public void SendEmployeeAssignmentToHistory()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                //TODO clarify with GK on what this means.
                //FOR A TERMINATION, THERE WOULD BE NO RECORDS IN THE EMPLOYEE_ASSIGNMENT_TABLE! THIS IS GK'S HACK - EUGIN TO 
                //EUGIN: NEED TO RETHINK THE LOGIC OR DO SOME CHECKING!
                #region - GK's HACK
                List<TransData> hack = Tables.Where(x => x.TableName == Constant.EMPLOYEE_ASSIGNMENT_TABLE).ToList();
                if (hack.Count == 0) return;
                #endregion

                if (Tables.Where(x => x.TableName == Constant.EMPLOYEE_ASSIGNMENT_TABLE).Count() == 0) return;
                TransData table = Tables.Single(x => x.TableName == Constant.EMPLOYEE_ASSIGNMENT_TABLE);
                //Send the old values to history
                XmlAttributeCollection oldAttributes = table.GetOldAttributesFromXml();
                EmployeeAssignmentHistory assignmentHistory = new EmployeeAssignmentHistory();
                assignmentHistory.EmployeeID = EmployeeId;
                if (oldAttributes.GetNamedItem("evidenceReferenceNo") != null) assignmentHistory.evidenceReferenceNo = oldAttributes.GetNamedItem("evidenceReferenceNo") == null ? 0 : int.Parse(oldAttributes.GetNamedItem("evidenceReferenceNo").Value);
                assignmentHistory.employementTermID = int.Parse(oldAttributes.GetNamedItem("employementTermID").Value);
                if (oldAttributes.GetNamedItem("governmentLevel") != null) assignmentHistory.governmentLevel = oldAttributes.GetNamedItem("governmentLevel") == null ? 0 : int.Parse(oldAttributes.GetNamedItem("governmentLevel").Value);
                if (oldAttributes.GetNamedItem("fundSource") != null) assignmentHistory.fundSource = int.Parse(oldAttributes.GetNamedItem("fundSource").Value);
                assignmentHistory.dateStart = DateTime.Parse(oldAttributes.GetNamedItem("dateStart").Value);
                assignmentHistory.jobKey = oldAttributes.GetNamedItem("jobKey") == null ? null : oldAttributes.GetNamedItem("jobKey").Value;
                assignmentHistory.jobID = int.Parse(oldAttributes.GetNamedItem("jobID").Value);
                assignmentHistory.jobTitleNominalRoll = oldAttributes.GetNamedItem("JobTitleNominalRoll") == null ? null : oldAttributes.GetNamedItem("JobTitleNominalRoll").Value;
                if (oldAttributes.GetNamedItem("jobTitleAgainst") != null) assignmentHistory.jobTitleAgainst = oldAttributes.GetNamedItem("jobTitleAgainst") == null ? null : oldAttributes.GetNamedItem("jobTitleAgainst").Value;
                assignmentHistory.payScaleID = int.Parse(oldAttributes.GetNamedItem("payScaleID").Value);
                assignmentHistory.jobGrade = int.Parse(oldAttributes.GetNamedItem("jobGrade").Value);
                assignmentHistory.jobGradeClassification = int.Parse(oldAttributes.GetNamedItem("jobGradeClassification").Value);
                assignmentHistory.paySegment = int.Parse(oldAttributes.GetNamedItem("paySegment").Value);
                assignmentHistory.payStatusID = int.Parse(oldAttributes.GetNamedItem("payStatusID").Value);
                assignmentHistory.IsPensionable = int.Parse(oldAttributes.GetNamedItem("IsPensionable").Value) == 1 ? true : false;
                assignmentHistory.mdaID = int.Parse(oldAttributes.GetNamedItem("mdaID").Value);
                assignmentHistory.directorateID = int.Parse(oldAttributes.GetNamedItem("directorateID").Value);
                if (oldAttributes.GetNamedItem("job_worklocation_StateID") != null) assignmentHistory.job_worklocation_StateID = int.Parse(oldAttributes.GetNamedItem("job_worklocation_StateID").Value);
                if (oldAttributes.GetNamedItem("FileIndex") != null) assignmentHistory.FileIndex = oldAttributes.GetNamedItem("FileIndex").Value;
                assignmentHistory.payType = int.Parse(oldAttributes.GetNamedItem("payType").Value);
                assignmentHistory.payMethod = int.Parse(oldAttributes.GetNamedItem("payMethod").Value);
                assignmentHistory.payFrequency = int.Parse(oldAttributes.GetNamedItem("payFrequency").Value);
                assignmentHistory.authorisedBy = oldAttributes.GetNamedItem("authorisedBy").Value;
                assignmentHistory.authorisedOn = DateTime.Parse(oldAttributes.GetNamedItem("authorisedOn").Value);
                assignmentHistory.eventId = int.Parse(oldAttributes.GetNamedItem("eventId").Value);
                assignmentHistory.workflowComplete = int.Parse(oldAttributes.GetNamedItem("workflowComplete").Value);
                assignmentHistory.dateCreated = DateTime.Parse(oldAttributes.GetNamedItem("dateCreated").Value);
                if (oldAttributes.GetNamedItem("dateUpdated") != null) assignmentHistory.dateUpdated = DateTime.Parse(oldAttributes.GetNamedItem("dateUpdated").Value);
                assignmentHistory.whoCreated = oldAttributes.GetNamedItem("whoCreated").Value;
                if (oldAttributes.GetNamedItem("whoUpdated") != null) assignmentHistory.whoUpdated = oldAttributes.GetNamedItem("whoUpdated").Value;
                if (oldAttributes.GetNamedItem("LogRefID") != null) assignmentHistory.LogRefID = oldAttributes.GetNamedItem("LogRefID").Value;

                ents.EmployeeAssignmentHistories.Add(assignmentHistory);
                ents.SaveChanges();
            }
        }

        /// <summary>
        /// If the employee assignment already exists, then do an update.
        /// </summary>
        public void InsertEmployeeAssignment()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                if (Tables.Where(x => x.TableName == Constant.EMPLOYEE_ASSIGNMENT_TABLE).Count() == 0) return;
                TransData table = Tables.Single(x => x.TableName == Constant.EMPLOYEE_ASSIGNMENT_TABLE);
                XmlAttributeCollection attributes = table.GetNewAttributesFromXml();
                EmployeeAssignment assignment;
                bool exists = false;
                if (table.HasOldValue())
                {
                    SendEmployeeAssignmentToHistory();
                    assignment = ents.EmployeeAssignments.Find(EmployeeId);
                    exists = true;
                }
                else
                {
                    assignment = new EmployeeAssignment();
                    assignment.EmployeeID = EmployeeId;
                }

                if (attributes.GetNamedItem("evidenceReferenceNo") != null) assignment.evidenceReferenceNo = int.Parse(attributes.GetNamedItem("evidenceReferenceNo").Value);
                assignment.employementTermID = int.Parse(attributes.GetNamedItem("employementTermID").Value);
                if (attributes.GetNamedItem("governmentLevel") != null) assignment.governmentLevel = int.Parse(attributes.GetNamedItem("governmentLevel").Value);
                if (attributes.GetNamedItem("fundSource") != null) assignment.fundSource = int.Parse(attributes.GetNamedItem("fundSource").Value);
                assignment.dateStart = DateTime.Parse(attributes.GetNamedItem("dateStart").Value);
                assignment.jobKey = attributes.GetNamedItem("jobKey").Value;
                assignment.jobID = int.Parse(attributes.GetNamedItem("jobID").Value);
                assignment.JobTitleNominalRoll = attributes.GetNamedItem("JobTitleNominalRoll").Value;
                if (attributes.GetNamedItem("jobTitleAgainst") != null) assignment.jobTitleAgainst = attributes.GetNamedItem("jobTitleAgainst").Value;
                assignment.payScaleID = int.Parse(attributes.GetNamedItem("payScaleID").Value);
                assignment.jobGrade = int.Parse(attributes.GetNamedItem("jobGrade").Value);
                assignment.jobGradeClassification = int.Parse(attributes.GetNamedItem("jobGradeClassification").Value);
                assignment.paySegment = int.Parse(attributes.GetNamedItem("paySegment").Value);
                assignment.payStatusID = int.Parse(attributes.GetNamedItem("payStatusID").Value);
                assignment.IsPensionable = int.Parse(attributes.GetNamedItem("IsPensionable").Value) == 1 ? true : false;
                assignment.mdaID = int.Parse(attributes.GetNamedItem("mdaID").Value);
                assignment.directorateID = int.Parse(attributes.GetNamedItem("directorateID").Value);
                if (attributes.GetNamedItem("job_worklocation_StateID") != null) assignment.job_worklocation_StateID = int.Parse(attributes.GetNamedItem("job_worklocation_StateID").Value);
                if (attributes.GetNamedItem("FileIndex") != null) assignment.FileIndex = attributes.GetNamedItem("FileIndex").Value;
                assignment.payType = int.Parse(attributes.GetNamedItem("payType").Value);
                assignment.payMethod = int.Parse(attributes.GetNamedItem("payMethod").Value);
                assignment.payFrequency = int.Parse(attributes.GetNamedItem("payFrequency").Value);
                assignment.authorisedBy = attributes.GetNamedItem("authorisedBy").Value;
                assignment.authorisedOn = DateTime.Parse(attributes.GetNamedItem("authorisedOn").Value);
                assignment.eventId = int.Parse(attributes.GetNamedItem("eventId").Value);
                assignment.workflowComplete = int.Parse(attributes.GetNamedItem("workflowComplete").Value);
                assignment.dateCreated = DateTime.Parse(attributes.GetNamedItem("dateCreated").Value);
                if (attributes.GetNamedItem("dateUpdated") != null) assignment.dateUpdated = DateTime.Parse(attributes.GetNamedItem("dateUpdated").Value);
                assignment.whoCreated = attributes.GetNamedItem("whoCreated").Value;
                if (attributes.GetNamedItem("whoUpdated") != null) assignment.whoUpdated = attributes.GetNamedItem("whoUpdated").Value;
                if (attributes.GetNamedItem("LogRefID") != null) assignment.LogRefID = attributes.GetNamedItem("LogRefID").Value;

                ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeAssignmentEventLog ON dbo.EmployeeAssignment");
                if (!exists) ents.EmployeeAssignments.Add(assignment);
                ents.SaveChanges();

                ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeAssignmentEventLog ON dbo.EmployeeAssignment");
            }
        }

        public void DeleteEmployeeAssignment()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeAssignmentEventLog ON dbo.EmployeeAssignment");
                ents.Database.ExecuteSqlCommand(string.Format("DELETE FROM dbo.EmployeeAssignment WHERE EmployeeID = {0}", EmployeeId));
                ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeAssignmentEventLog ON dbo.EmployeeAssignment");
            }
        }

        /// <summary>
        /// If old value exists in transData, then revert to it otherwise delete the employee assignment.
        /// </summary>
        public void UndoEmployeeAssignment()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                if (Tables.Where(x => x.TableName == Constant.EMPLOYEE_ASSIGNMENT_TABLE).Count() == 0) return;
                TransData table = Tables.Single(x => x.TableName == Constant.EMPLOYEE_ASSIGNMENT_TABLE);
                //if old value exists, use it
                if (table.HasOldValue())
                {
                    XmlAttributeCollection attributes = table.GetOldAttributesFromXml();
                    EmployeeAssignment assignment = ents.EmployeeAssignments.Find(EmployeeId);

                    if (attributes.GetNamedItem("evidenceReferenceNo") != null) assignment.evidenceReferenceNo = int.Parse(attributes.GetNamedItem("evidenceReferenceNo").Value);
                    assignment.employementTermID = int.Parse(attributes.GetNamedItem("employementTermID").Value);
                    if (attributes.GetNamedItem("governmentLevel") != null) assignment.governmentLevel = int.Parse(attributes.GetNamedItem("governmentLevel").Value);
                    if (attributes.GetNamedItem("fundSource") != null) assignment.fundSource = int.Parse(attributes.GetNamedItem("fundSource").Value);
                    assignment.dateStart = DateTime.Parse(attributes.GetNamedItem("dateStart").Value);
                    assignment.jobKey = attributes.GetNamedItem("jobKey").Value;
                    assignment.jobID = int.Parse(attributes.GetNamedItem("jobID").Value);
                    assignment.JobTitleNominalRoll = attributes.GetNamedItem("JobTitleNominalRoll").Value;
                    if (attributes.GetNamedItem("jobTitleAgainst") != null) assignment.jobTitleAgainst = attributes.GetNamedItem("jobTitleAgainst").Value;
                    assignment.payScaleID = int.Parse(attributes.GetNamedItem("payScaleID").Value);
                    assignment.jobGrade = int.Parse(attributes.GetNamedItem("jobGrade").Value);
                    assignment.jobGradeClassification = int.Parse(attributes.GetNamedItem("jobGradeClassification").Value);
                    assignment.paySegment = int.Parse(attributes.GetNamedItem("paySegment").Value);
                    assignment.payStatusID = int.Parse(attributes.GetNamedItem("payStatusID").Value);
                    assignment.IsPensionable = int.Parse(attributes.GetNamedItem("IsPensionable").Value) == 1 ? true : false;
                    assignment.mdaID = int.Parse(attributes.GetNamedItem("mdaID").Value);
                    assignment.directorateID = int.Parse(attributes.GetNamedItem("directorateID").Value);
                    if (attributes.GetNamedItem("job_worklocation_StateID") != null) assignment.job_worklocation_StateID = int.Parse(attributes.GetNamedItem("job_worklocation_StateID").Value);
                    if (attributes.GetNamedItem("FileIndex") != null) assignment.FileIndex = attributes.GetNamedItem("FileIndex").Value;
                    assignment.payType = int.Parse(attributes.GetNamedItem("payType").Value);
                    assignment.payMethod = int.Parse(attributes.GetNamedItem("payMethod").Value);
                    assignment.payFrequency = int.Parse(attributes.GetNamedItem("payFrequency").Value);
                    assignment.authorisedBy = attributes.GetNamedItem("authorisedBy").Value;
                    assignment.authorisedOn = DateTime.Parse(attributes.GetNamedItem("authorisedOn").Value);
                    assignment.eventId = int.Parse(attributes.GetNamedItem("eventId").Value);
                    assignment.workflowComplete = int.Parse(attributes.GetNamedItem("workflowComplete").Value);
                    assignment.dateCreated = DateTime.Parse(attributes.GetNamedItem("dateCreated").Value);
                    if (attributes.GetNamedItem("dateUpdated") != null) assignment.dateUpdated = DateTime.Parse(attributes.GetNamedItem("dateUpdated").Value);
                    assignment.whoCreated = attributes.GetNamedItem("whoCreated").Value;
                    if (attributes.GetNamedItem("whoUpdated") != null) assignment.whoUpdated = attributes.GetNamedItem("whoUpdated").Value;
                    if (attributes.GetNamedItem("LogRefID") != null) assignment.LogRefID = attributes.GetNamedItem("LogRefID").Value;


                    ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeAssignmentEventLog ON dbo.EmployeeAssignment");

                    ents.SaveChanges();

                    ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeAssignmentEventLog ON dbo.EmployeeAssignment");
                }
                else
                {
                    DeleteEmployeeAssignment();
                }
            }
        }

        public void InsertPayItems()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                foreach (TransData table in Tables.Where(x => x.TableName == Constant.EMPLOYEE_PAYITEM_TABLE && x.ActionType == Constant.TRANSDATA_ACTION_TYPE_DELETE))
                {
                    XmlAttributeCollection attributes = table.GetOldAttributesFromXml();

                    ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");
                    ents.Database.ExecuteSqlCommand(string.Format("DELETE FROM dbo.EmployeePayItem WHERE EmployeeID = {0} AND payItemID = {1}", EmployeeId, int.Parse(attributes.GetNamedItem("payItemID").Value)));
                    ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");
                }

                foreach (TransData table in Tables.Where(x => x.TableName == Constant.EMPLOYEE_PAYITEM_TABLE && x.ActionType != Constant.TRANSDATA_ACTION_TYPE_DELETE))
                {
                    XmlAttributeCollection attributes = table.GetNewAttributesFromXml();
                    EmployeePayItem payItem = new EmployeePayItem();

                    payItem.EmployeeID = EmployeeId;
                    payItem.payItemID = int.Parse(attributes.GetNamedItem("payItemID").Value);
                    if (attributes.GetNamedItem("startPeriod") != null) payItem.startPeriod = int.Parse(attributes.GetNamedItem("startPeriod").Value);
                    if (attributes.GetNamedItem("endPeriod") != null) payItem.endPeriod = int.Parse(attributes.GetNamedItem("endPeriod").Value);
                    if (attributes.GetNamedItem("paramValue") != null) payItem.paramValue = double.Parse(attributes.GetNamedItem("paramValue").Value);
                    payItem.dateCreated = DateTime.Parse(attributes.GetNamedItem("dateCreated").Value);
                    if (attributes.GetNamedItem("dateUpdated") != null) payItem.dateUpdated = DateTime.Parse(attributes.GetNamedItem("dateUpdated").Value);
                    payItem.whoCreated = attributes.GetNamedItem("whoCreated").Value;
                    if (attributes.GetNamedItem("whoUpdated") != null) payItem.whoUpdated = attributes.GetNamedItem("whoUpdated").Value;
                    if (attributes.GetNamedItem("LogRefID") != null) payItem.LogRefID = attributes.GetNamedItem("LogRefID").Value;

                    ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");

                    ents.EmployeePayItems.Add(payItem);
                    ents.SaveChanges();

                    ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");
                }
            }
        }

        public void DeletePayItems()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");
                ents.Database.ExecuteSqlCommand(string.Format("DELETE FROM dbo.EmployeePayItem WHERE EmployeeID = {0}", EmployeeId));
                ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");
            }
        }

        /// <summary>
        /// If old value exists in transData, then revert to it otherwise delete the employee payitems.
        /// </summary>
        public void UndoPayItems()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                foreach (TransData table in Tables.Where(x => x.TableName == Constant.EMPLOYEE_PAYITEM_TABLE && x.ActionType == Constant.TRANSDATA_ACTION_TYPE_INSERT))
                {
                    //Remove all those inserted
                    XmlAttributeCollection attributes = table.GetNewAttributesFromXml();
                    int payItemId = int.Parse(attributes.GetNamedItem("payItemID").Value);
                    EmployeePayItem payItem = ents.EmployeePayItems.Single(x => x.EmployeeID == EmployeeId && x.payItemID == payItemId);
                    ents.EmployeePayItems.Remove(payItem);

                    ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");

                    ents.SaveChanges();

                    ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");
                }
                foreach (TransData table in Tables.Where(x => x.TableName == Constant.EMPLOYEE_PAYITEM_TABLE && x.ActionType == Constant.TRANSDATA_ACTION_TYPE_DELETE))
                {
                    //Restore the deleted ones

                    XmlAttributeCollection attributesOld = table.GetOldAttributesFromXml();
                    EmployeePayItem payItemOld = new EmployeePayItem();

                    payItemOld.EmployeeID = EmployeeId;
                    payItemOld.payItemID = int.Parse(attributesOld.GetNamedItem("payItemID").Value);
                    if (attributesOld.GetNamedItem("startPeriod") != null) payItemOld.startPeriod = int.Parse(attributesOld.GetNamedItem("startPeriod").Value);
                    if (attributesOld.GetNamedItem("endPeriod") != null) payItemOld.endPeriod = int.Parse(attributesOld.GetNamedItem("endPeriod").Value);
                    if (attributesOld.GetNamedItem("paramValue") != null) payItemOld.paramValue = double.Parse(attributesOld.GetNamedItem("paramValue").Value);
                    payItemOld.dateCreated = DateTime.Parse(attributesOld.GetNamedItem("dateCreated").Value);
                    if (attributesOld.GetNamedItem("dateUpdated") != null) payItemOld.dateUpdated = DateTime.Parse(attributesOld.GetNamedItem("dateUpdated").Value);
                    payItemOld.whoCreated = attributesOld.GetNamedItem("whoCreated").Value;
                    if (attributesOld.GetNamedItem("whoUpdated") != null) payItemOld.whoUpdated = attributesOld.GetNamedItem("whoUpdated").Value;
                    if (attributesOld.GetNamedItem("LogRefID") != null) payItemOld.LogRefID = attributesOld.GetNamedItem("LogRefID").Value;
                    ents.EmployeePayItems.Add(payItemOld);

                    ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");

                    ents.SaveChanges();

                    ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");

                }
                    //switch (table.ActionType)
                    //{
                    //    case Constant.TRANSDATA_ACTION_TYPE_INSERT:
                    //        //Remove all those inserted
                    //        XmlAttributeCollection attributes = table.GetNewAttributesFromXml();
                    //        int payItemId = int.Parse(attributes.GetNamedItem("payItemID").Value);
                    //        EmployeePayItem payItem = ents.EmployeePayItems.Single(x => x.EmployeeID == EmployeeId && x.payItemID == payItemId);
                    //        ents.EmployeePayItems.Remove(payItem);
                    //        break;
                    //    case Constant.TRANSDATA_ACTION_TYPE_DELETE:
                    //        //Restore the deleted ones

                    //        XmlAttributeCollection attributesOld = table.GetOldAttributesFromXml();
                    //        EmployeePayItem payItemOld = new EmployeePayItem();

                    //        payItemOld.EmployeeID = EmployeeId;
                    //        payItemOld.payItemID = int.Parse(attributesOld.GetNamedItem("payItemID").Value);
                    //        if (attributesOld.GetNamedItem("startPeriod") != null) payItemOld.startPeriod = int.Parse(attributesOld.GetNamedItem("startPeriod").Value);
                    //        if (attributesOld.GetNamedItem("endPeriod") != null) payItemOld.endPeriod = int.Parse(attributesOld.GetNamedItem("endPeriod").Value);
                    //        if (attributesOld.GetNamedItem("paramValue") != null) payItemOld.paramValue = double.Parse(attributesOld.GetNamedItem("paramValue").Value);
                    //        payItemOld.dateCreated = DateTime.Parse(attributesOld.GetNamedItem("dateCreated").Value);
                    //        if (attributesOld.GetNamedItem("dateUpdated") != null) payItemOld.dateUpdated = DateTime.Parse(attributesOld.GetNamedItem("dateUpdated").Value);
                    //        payItemOld.whoCreated = attributesOld.GetNamedItem("whoCreated").Value;
                    //        if (attributesOld.GetNamedItem("whoUpdated") != null) payItemOld.whoUpdated = attributesOld.GetNamedItem("whoUpdated").Value;
                    //        if (attributesOld.GetNamedItem("LogRefID") != null) payItemOld.LogRefID = attributesOld.GetNamedItem("LogRefID").Value;
                    //        ents.EmployeePayItems.Add(payItemOld);
                    //        break;
                    //}

                    //ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");

                    //ents.SaveChanges();

                    //ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeePayItemEventLog ON dbo.EmployeePayItem");

                }
            
        }

        public void InsertEmployeeBankAccount()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                foreach (TransData table in Tables.Where(x => x.TableName == Constant.EMPLOYEE_BANK_ACCOUNT_TABLE))
                {
                    XmlAttributeCollection attributes = table.GetNewAttributesFromXml();
                    EmployeeBankAccount employeeBankAccount = new EmployeeBankAccount();

                    employeeBankAccount.EmployeeID = EmployeeId;
                    employeeBankAccount.BranchID = int.Parse(attributes.GetNamedItem("BranchID").Value);
                    employeeBankAccount.AccountNumber = attributes.GetNamedItem("AccountNumber").Value;
                    employeeBankAccount.AccountName = attributes.GetNamedItem("AccountName").Value;
                    employeeBankAccount.dateCreated = DateTime.Parse(attributes.GetNamedItem("dateCreated").Value);
                    if (attributes.GetNamedItem("dateUpdated") != null) employeeBankAccount.dateUpdated = DateTime.Parse(attributes.GetNamedItem("dateUpdated").Value);
                    employeeBankAccount.whoCreated = attributes.GetNamedItem("whoCreated").Value;
                    if (attributes.GetNamedItem("whoUpdated") != null) employeeBankAccount.whoUpdated = attributes.GetNamedItem("whoUpdated").Value;
                    if (attributes.GetNamedItem("LogRefID") != null) employeeBankAccount.LogRefID = attributes.GetNamedItem("LogRefID").Value;


                    ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeBankAccountEventLog ON dbo.EmployeeBankAccount");

                    ents.EmployeeBankAccounts.Add(employeeBankAccount);
                    ents.SaveChanges();

                    ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeBankAccountEventLog ON dbo.EmployeeBankAccount");
                }
            }
        }

        public void DeleteEmployeeBankAccount()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeBankAccountEventLog ON dbo.EmployeeBankAccount");
                ents.Database.ExecuteSqlCommand(string.Format("DELETE FROM dbo.EmployeeBankAccount WHERE EmployeeID = {0}", EmployeeId));
                ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeBankAccountEventLog ON dbo.EmployeeBankAccount");
            }
        }

        /// <summary>
        /// If old value exists in transData, then revert to it otherwise delete the employee bank account.
        /// </summary>
        public void UndoEmployeeBankAccount()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                foreach (TransData table in Tables.Where(x => x.TableName == Constant.EMPLOYEE_BANK_ACCOUNT_TABLE))
                {
                    if (table.HasOldValue())
                    {
                        XmlAttributeCollection attributes = table.GetOldAttributesFromXml();

                        int branchID = int.Parse(attributes.GetNamedItem("BranchID").Value);
                        EmployeeBankAccount employeeBankAccount = ents.EmployeeBankAccounts.Single(x => x.EmployeeID == EmployeeId && x.BranchID == branchID);

                        employeeBankAccount.EmployeeID = EmployeeId;
                        employeeBankAccount.BranchID = branchID;
                        employeeBankAccount.AccountNumber = attributes.GetNamedItem("whoCreated").Value;
                        employeeBankAccount.AccountName = attributes.GetNamedItem("whoCreated").Value;
                        employeeBankAccount.dateCreated = DateTime.Parse(attributes.GetNamedItem("dateCreated").Value);
                        employeeBankAccount.dateUpdated = DateTime.Parse(attributes.GetNamedItem("dateUpdated").Value);
                        employeeBankAccount.whoCreated = attributes.GetNamedItem("whoCreated").Value;
                        employeeBankAccount.whoUpdated = attributes.GetNamedItem("whoUpdated").Value;
                        employeeBankAccount.LogRefID = attributes.GetNamedItem("LogRefID").Value;

                        ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeBankAccountEventLog ON dbo.EmployeeBankAccount");

                        ents.SaveChanges();

                        ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeBankAccountEventLog ON dbo.EmployeeBankAccount");
                    }
                    else
                    {
                        DeleteEmployeeBankAccount();
                    }
                }
            }
        }

        /// <summary>
        /// If the employee event exists, then do an update.
        /// </summary>
        public void InsertEmployeeEvent()
        {//TODO what happens when multiple events are sent?
            bool exists = false;
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                if (Tables.Where(x => x.TableName == Constant.EMPLOYEE_EVENT_TABLE).Count() == 0) return;
                XmlAttributeCollection attributes = Tables.Single(x => x.TableName == Constant.EMPLOYEE_EVENT_TABLE).GetNewAttributesFromXml();
                EmployeeEvent empEvent;
                if (attributes.GetNamedItem("employeeEventID") != null)
                {
                    int employeeEventID = int.Parse(attributes.GetNamedItem("employeeEventID").Value);
                    if (ents.EmployeeEvents.Where(x => x.employeeEventID == employeeEventID).Count() > 0)
                    {
                        empEvent = ents.EmployeeEvents.Find(employeeEventID);
                        exists = true;
                    }
                    else
                    {
                        empEvent = new EmployeeEvent();
                        empEvent.EmployeeID = EmployeeId;
                        empEvent.employeeEventID = employeeEventID;
                    }

                    empEvent.EventID = int.Parse(attributes.GetNamedItem("EventID").Value);
                    if (attributes.GetNamedItem("EventTypeID") != null) empEvent.EventTypeID = int.Parse(attributes.GetNamedItem("EventTypeID").Value);
                    if (attributes.GetNamedItem("EventDescription") != null) empEvent.EventDescription = attributes.GetNamedItem("EventDescription").Value;
                    empEvent.EffectOnSalary = int.Parse(attributes.GetNamedItem("EffectOnSalary").Value);
                    empEvent.EventDate = DateTime.Parse(attributes.GetNamedItem("EventDate").Value);
                    if (attributes.GetNamedItem("StartPeriod") != null) empEvent.StartPeriod = int.Parse(attributes.GetNamedItem("StartPeriod").Value);
                    if (attributes.GetNamedItem("EndPeriod") != null) empEvent.EndPeriod = int.Parse(attributes.GetNamedItem("EndPeriod").Value);
                    empEvent.status = int.Parse(attributes.GetNamedItem("status").Value) == 1 ? true : false;
                    if (attributes.GetNamedItem("payItemID") != null) empEvent.payItemID = int.Parse(attributes.GetNamedItem("payItemID").Value);
                    if (attributes.GetNamedItem("jobKey") != null) empEvent.jobKey = attributes.GetNamedItem("jobKey").Value;
                    if (attributes.GetNamedItem("ActingGrade") != null) empEvent.ActingGrade = int.Parse(attributes.GetNamedItem("ActingGrade").Value);
                    empEvent.authorizedBy = attributes.GetNamedItem("authorizedBy").Value;
                    empEvent.authorizedOn = DateTime.Parse(attributes.GetNamedItem("authorizedOn").Value);



                    empEvent.dateCreated = DateTime.Parse(attributes.GetNamedItem("dateCreated").Value);
                    if (attributes.GetNamedItem("dateUpdated") != null) empEvent.dateUpdated = DateTime.Parse(attributes.GetNamedItem("dateUpdated").Value);
                    empEvent.whoCreated = attributes.GetNamedItem("whoCreated").Value;
                    if (attributes.GetNamedItem("whoUpdated") != null) empEvent.whoUpdated = attributes.GetNamedItem("whoUpdated").Value;
                    if (attributes.GetNamedItem("LogRefID") != null) empEvent.LogRefID = attributes.GetNamedItem("LogRefID").Value;

                    ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeEvent_EventLog ON dbo.EmployeeEvent");
                    if (!exists) ents.EmployeeEvents.Add(empEvent);
                    ents.SaveChanges();
                    ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeEvent_EventLog ON dbo.EmployeeEvent");
                }
            }
        }

        public void DeleteEmployeeEvent(int employeeEventId)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeEvent_EventLog ON dbo.EmployeeEvent");
                ents.Database.ExecuteSqlCommand(string.Format("DELETE FROM dbo.EmployeeEvent WHERE employeeEventID = {0}", employeeEventId));
                ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeEvent_EventLog ON dbo.EmployeeEvent");
            }
        }

        /// <summary>
        /// If old value exists in transData, then revert to it otherwise delete the employee event.
        /// </summary>
        public void UndoEmployeeEvent()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                if (Tables.Where(x => x.TableName == Constant.EMPLOYEE_EVENT_TABLE).Count() == 0) return;
                TransData table = Tables.Single(x => x.TableName == Constant.EMPLOYEE_EVENT_TABLE);

                if (table.HasOldValue())
                {
                    XmlAttributeCollection attributes = table.GetOldAttributesFromXml();

                    int employeeEventID = int.Parse(attributes.GetNamedItem("employeeEventID").Value);

                    EmployeeEvent empEvent = ents.EmployeeEvents.Find(employeeEventID);

                    empEvent.EmployeeID = employeeEventID;
                    empEvent.EmployeeID = EmployeeId;
                    empEvent.EventID = int.Parse(attributes.GetNamedItem("EventID").Value);
                    if (attributes.GetNamedItem("EventTypeID") != null) empEvent.EventTypeID = int.Parse(attributes.GetNamedItem("EventTypeID").Value);
                    if (attributes.GetNamedItem("EventDescription") != null) empEvent.EventDescription = attributes.GetNamedItem("EventDescription").Value;
                    empEvent.EffectOnSalary = int.Parse(attributes.GetNamedItem("EffectOnSalary").Value);
                    empEvent.EventDate = DateTime.Parse(attributes.GetNamedItem("EventDate").Value);
                    if (attributes.GetNamedItem("StartPeriod") != null) empEvent.StartPeriod = int.Parse(attributes.GetNamedItem("StartPeriod").Value);
                    if (attributes.GetNamedItem("EndPeriod") != null) empEvent.EndPeriod = int.Parse(attributes.GetNamedItem("EndPeriod").Value);
                    empEvent.status = bool.Parse(attributes.GetNamedItem("status").Value);
                    if (attributes.GetNamedItem("payItemID") != null) empEvent.payItemID = int.Parse(attributes.GetNamedItem("payItemID").Value);
                    if (attributes.GetNamedItem("jobKey") != null) empEvent.jobKey = attributes.GetNamedItem("jobKey").Value;
                    if (attributes.GetNamedItem("ActingGrade") != null) empEvent.ActingGrade = int.Parse(attributes.GetNamedItem("ActingGrade").Value);
                    empEvent.authorizedBy = attributes.GetNamedItem("authorizedBy").Value;
                    empEvent.authorizedOn = DateTime.Parse(attributes.GetNamedItem("authorizedOn").Value);



                    empEvent.dateCreated = DateTime.Parse(attributes.GetNamedItem("dateCreated").Value);
                    if (attributes.GetNamedItem("dateUpdated") != null) empEvent.dateUpdated = DateTime.Parse(attributes.GetNamedItem("dateUpdated").Value);
                    empEvent.whoCreated = attributes.GetNamedItem("whoCreated").Value;
                    if (attributes.GetNamedItem("whoUpdated") != null) empEvent.whoUpdated = attributes.GetNamedItem("whoUpdated").Value;
                    if (attributes.GetNamedItem("LogRefID") != null) empEvent.LogRefID = attributes.GetNamedItem("LogRefID").Value;

                    ents.Database.ExecuteSqlCommand("DISABLE TRIGGER dbo.EmployeeEvent_EventLog ON dbo.EmployeeEvent");
                    ents.SaveChanges();
                    ents.Database.ExecuteSqlCommand("ENABLE TRIGGER dbo.EmployeeEvent_EventLog ON dbo.EmployeeEvent");
                }
                else
                {
                    XmlAttributeCollection attributes = table.GetNewAttributesFromXml();

                    int employeeEventID = int.Parse(attributes.GetNamedItem("employeeEventID").Value);
                    DeleteEmployeeEvent(employeeEventID);
                }
            }
        }

        public void ClearTransData()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                ents.Database.ExecuteSqlCommand(string.Format("DELETE FROM dbo.TransData WHERE EmployeeID = {0}", EmployeeId));
            }
        }

        #endregion

        public void Save(SyncType syncType)
        {

            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    switch (syncType)
                    {
                        case SyncType.Consolidate:
                            if (SiteConfigServices.IsManagedMDA(MdaId))
                            {
                                //If approved, then move the Assignment to history
                                //TODO test this well later
                                SendEmployeeAssignmentToHistory();
                                ClearTransData();
                            }
                            else
                            {
                                InsertEmployee();
                                InsertEmployeeAssignment();
                                InsertPayItems();
                                InsertEmployeeBankAccount();
                                InsertEmployeeEvent();
                            }
                            break;
                        case SyncType.Approval:
                            if (SiteConfigServices.IsManagedMDA(MdaId))
                            {
                                if (State == SyncState.Rejected)//Rejected
                                {
                                    UndoEmployeeBankAccount();
                                    UndoPayItems();
                                    UndoEmployeeAssignment();
                                    UndoEmployee();
                                }
                                //ClearTransData();
                            }
                            break;
                    }
                    scope.Complete();
                }
            }
        }
    }
}
