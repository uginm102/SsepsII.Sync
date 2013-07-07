using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SsepsII.Synchronisation.DAL
{
    public partial class SystemUser
    {
        public bool HasSingleMDA
        {
            get
            {
                return SystemUserMdas.Count == 1;
            }
        }

        public bool SupervisesSingleMDA
        {
            get
            {
                return SystemUserSupervisedMdas.Count == 1;
            }
        }

        public bool HasSingleGovernment
        {
            get
            {
                if (HasSingleMDA) return true;
                int currentGovernmentID = 0;
                foreach (SystemUserMda sysUserMDA in SystemUserMdas)
                {
                    if (currentGovernmentID == 0)
                    {
                        currentGovernmentID = sysUserMDA.MdaGovernmentMapping.governmentID;
                        continue;
                    }
                    if (currentGovernmentID != sysUserMDA.MdaGovernmentMapping.governmentID) return false;
                    currentGovernmentID = sysUserMDA.MdaGovernmentMapping.governmentID;
                }
                return true;
            }
        }

        public bool HasSingleGovernmentLevel
        {
            get
            {
                if (HasSingleGovernment) return true;
                int currentGovernmentLevelID = 0;
                foreach (SystemUserMda sysUserMDA in SystemUserMdas)
                {
                    if (currentGovernmentLevelID == 0)
                    {
                        currentGovernmentLevelID = sysUserMDA.MdaGovernmentMapping.Government.governmentLevelID;
                        continue;
                    }
                    if (currentGovernmentLevelID != sysUserMDA.MdaGovernmentMapping.Government.governmentLevelID) return false;
                    currentGovernmentLevelID = sysUserMDA.MdaGovernmentMapping.Government.governmentLevelID;
                }
                return true;
            }
        }

        public List<MdaGovernmentMapping> AssignedMdaList
        {
            get
            {
                return SystemUserMdas.Select(s => s.MdaGovernmentMapping).OrderBy(m => m.mdaName).ToList();
            }
        }

        public List<MdaGovernmentMapping> SupervisedMdaList
        {
            get
            {
                return SystemUserSupervisedMdas.Select(s => s.MdaGovernmentMapping).OrderBy(m => m.mdaName).ToList();
            }
        }

        public List<int> AssignedMdaIdList
        {
            get
            {
                return this.AssignedMdaList.Select(m => m.mdaID).ToList();
            }
        }

        public List<int> SupervisedMdaIdList
        {
            get
            {
                return this.SupervisedMdaList.Select(m => m.mdaID).ToList();
            }
        }

        public List<int> GovernmentLevelIdList
        {
            get
            {
                return this.AssignedMdaList.Select(m => m.Government.governmentLevelID).ToList();
            }
        }
    }
}
