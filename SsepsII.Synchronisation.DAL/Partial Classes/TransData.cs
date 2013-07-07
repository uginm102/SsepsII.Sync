using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SsepsII.Synchronisation.DAL
{
    public partial class TransData
    {
        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}", RowGuid, ActionType, TableName, EmployeeID, LogRefID, SyncPeriod, Username, MdaId, state);
        }
    }
}
