using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SsepsII.Synchronisation.DAL
{
    public partial class EmployeePayrollHistory
    {
        public XElement AsXmlRow
        {
            get
            {
                return new XElement("Row",
                    new XElement("EmployeeID", EmployeeID),
                    new XElement("payrollPeriod", payrollPeriod),
                    new XElement("mdaID", mdaID),
                    new XElement("directorateID", directorateID),
                    new XElement("payrollDate", payrollDate),
                    new XElement("payItemsXML", XElement.Parse(payItemsXML)),
                    new XElement("grossPay", grossPay),
                    new XElement("netPay", netPay),
                    new XElement("payrollPreparedBy", payrollPreparedBy),
                    new XElement("payrollVerifiedBy", payrollVerifiedBy),
                    new XElement("payrollApprovedBy", payrollApprovedBy),
                    new XElement("dateCreated", dateCreated),
                    new XElement("datePosted", datePosted),
                    new XElement("whoCreated", whoCreated),
                    new XElement("whoPosted", whoPosted),
                    new XElement("LogRefID", LogRefID)
                    );
            }
        }
    }
}
