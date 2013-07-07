using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SsepsII.Synchronisation.Services
{
    public class ApprovalBO
    {
        public long EmployeeID { get; set; }
        public long State { get; set; }

        #region Main Display properties

        public string PIN { get; set; }
        public string Name { get; set; }
        public string Jobtitle { get; set; }

        public string TextToDisplay
        {
            get
            {
                return string.Format("{0} {1} {2}", PIN, Name, Jobtitle);
            }
        }
        #endregion

        public List<ApprovalNodeBO> ApprovalNodes;
        private ApprovalBO() { }
        public ApprovalBO(long employeeID)
            : this(employeeID, string.Empty, string.Empty, string.Empty)
        {
        }
        public ApprovalBO(long employeeID,string pin, string name, string jobTitle)
        {
            EmployeeID = employeeID;
            PIN = pin;
            Name = name;
            Jobtitle = jobTitle;
            ApprovalNodes = new List<ApprovalNodeBO>();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", EmployeeID, PIN, Name, Jobtitle, ApprovalNodes.Count);
        }
    }
}
