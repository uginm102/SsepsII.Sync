using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SsepsII.Synchronisation.DAL
{
    public partial class TransferredEmployee
    {
        public XDocument TransferXDocument
        {
            get
            {
                return XDocument.Parse(TransferXML);
            }
        }
    }
}
