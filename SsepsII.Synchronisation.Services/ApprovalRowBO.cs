using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SsepsII.Synchronisation.Services
{
    public class ApprovalRowBO
    {
        public string LogRefID { get; set; }
        public int MdaId { get; set; }
        public int State { get; set; }
        public string NewValue { get; set; }

        public XElement AsXmlRow
        {
            get
            {
                return new XElement("Row",
                    new XElement("LogRefID", LogRefID),
                    new XElement("MdaId", MdaId),
                    new XElement("state", State),
                    new XElement("newValue", XElement.Parse(NewValue))
                    );
            }
        }
    }
}
