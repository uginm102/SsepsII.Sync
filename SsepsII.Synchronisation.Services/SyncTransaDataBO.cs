using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SsepsII.Synchronisation.Services
{
    public class SyncTransaDataBO
    {
        public long RowGuid { get; set; }
        public string ActionType { get; set; }
        public string TableName { get; set; }
        public System.DateTime TransDate { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public long EmployeeID { get; set; }
        public string LogRefID { get; set; }
        public int SyncPeriod { get; set; }
        public string Username { get; set; }
        public int MdaId { get; set; }
        public int state { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", RowGuid, ActionType, TableName, EmployeeID, LogRefID, SyncPeriod, Username, MdaId, state);
        }
    }
}
