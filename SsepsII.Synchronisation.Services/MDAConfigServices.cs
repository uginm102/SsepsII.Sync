using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Synchronisation.DAL;

namespace SsepsII.Synchronisation.Services
{
    public static class MDAConfigServices
    {
        public static int GetCurrentPayrollPeriodForMDAByMDAId(int mdaId)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {              
                MDA_CONFIG md = ents.MDA_CONFIG.FirstOrDefault(x => x.MDAID == mdaId);
                return md == null ? 0 : int.Parse( md.CONFIG_VALUE);              
            }
        }
        
        public static int GetCurrentPayrollPeriodForMDAByEmployeeId(long empId)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                return GetCurrentPayrollPeriodForMDAByMDAId(ents.EmployeeAssignments.FirstOrDefault(a => a.EmployeeID == empId).mdaID);
            }
        }
    }
}
