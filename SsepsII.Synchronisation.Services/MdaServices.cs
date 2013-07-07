using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Synchronisation.DAL;
using SsepsII;

namespace SsepsII.Synchronisation.Services
{
    public class MdaBO
    {
        public int MdaStructureID { get; set; }
        public string MdaStructureName { get; set; }
        public string MdaStructureType { get; set; }
        public string Sector { get; set; }
    }

    public class MdaGovernmentBO
    {
        public int mdaID { get; set; }
        public string mdaName { get; set; }
        public string governmentName { get; set; }

    }
   
    public class MdaServices
    {
        public MdaGovernmentMapping GetMdaById(int mdaId)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                return ents.MdaGovernmentMappings.Include("Government").FirstOrDefault(x => x.mdaID == mdaId);
            }
        }

        //public DirectorateListing GetDirectorateById(int dirId)
        //{
        //    using (var context = new SsepsIISynEntities())
        //    {
        //        return context.DirectorateListings.Include("MdaGovernmentMapping.Government").FirstOrDefault(d => d.directorateID == dirId);
        //    }
        //}

        public List<MdaGovernmentMapping> GetALLMda()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                return ents.MdaGovernmentMappings.Include("Government").Include("MdaStructure").OrderBy(m => m.Government.governmentLevelID).ThenBy(m => m.Government.governmentName).ThenBy(x => x.mdaName).ToList();
            }
        }
        public List<MdaGovernmentMapping> GetALLMdaByGovtID(int govtID)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                return ents.MdaGovernmentMappings.Include("Government").Include("MdaStructure").Where(x=>x.governmentID==govtID).OrderBy(m => m.Government.governmentLevelID).ThenBy(m => m.Government.governmentName).ThenBy(x => x.mdaName).ToList();
            }
        }

        public List<MdaGovernmentMapping> GetMDAsWithAssignments()
        {
            using (var context = new SsepsIISynEntities())
            {
                return (from mdas in context.MdaGovernmentMappings
                        join empAsgn in context.EmployeeAssignments on mdas.mdaID equals empAsgn.mdaID
                        where mdas.mdaID == empAsgn.mdaID
                        orderby mdas.mdaName
                        select mdas).Distinct().ToList<MdaGovernmentMapping>();
            }
        }

        public List<MdaGovernmentBO> GetMyMDAGovtWithAssignments()
        {
            List<MdaGovernmentBO> temp = new List<MdaGovernmentBO>();
            using (var context = new SsepsIISynEntities())
            {
                var mds = (from mdas in context.MdaGovernmentMappings.Include("MdaGovernmentMapping.Government")
                        join empAsgn in context.EmployeeAssignments on mdas.mdaID equals empAsgn.mdaID
                        where mdas.mdaID == empAsgn.mdaID
                        orderby mdas.mdaName
                        select mdas).Distinct().ToList<MdaGovernmentMapping>();
                foreach (MdaGovernmentMapping mgm in mds)
                {
                    temp.Add(new MdaGovernmentBO()
                   {
                       governmentName=mgm.Government.governmentName,
                       mdaID=mgm.mdaID,
                       mdaName=mgm.mdaName
                   });
                }
            }
            return temp;
        }

        public List<MdaGovernmentMapping> GetGovtMDAsWithAssignments(int govtId)
        {
            using (var context = new SsepsIISynEntities())
            {
                return (from mdas in context.MdaGovernmentMappings
                        join assgn in context.EmployeeAssignments on mdas.mdaID equals assgn.mdaID
                        where (mdas.mdaID == assgn.mdaID && mdas.Government.governmentID == govtId)
                        select mdas).Distinct().OrderBy(m => m.mdaName).ToList<MdaGovernmentMapping>();
            }
        }

        public List<MdaGovernmentMapping> GetGovtMDAs(int govtId)
        {
            using (var context = new SsepsIISynEntities())
            {
                return context.MdaGovernmentMappings.Include("Government").Where(m => (m.Government.governmentID == govtId)).OrderBy(m => m.mdaName).ToList();
               
            }
        }

        public List<MdaGovernmentMapping> GetGovtMDAs(int govtId, bool IncludeCountyMDAs=false)
        {
            using (var context = new SsepsIISynEntities())
            {
                return IncludeCountyMDAs ? context.MdaGovernmentMappings.Include("Government").Where(m => (m.Government.governmentID == govtId || m.Government.linkToID == govtId)).OrderBy(m => m.mdaName).ToList() : context.MdaGovernmentMappings.Include("Government").Where(m => (m.Government.governmentID == govtId)).OrderBy(m => m.mdaName).ToList(); 

            }
        }

        public List<MdaGovernmentMapping> GetGovtLevelMDAs(int govtLevelId)
        {
            using (var context = new SsepsIISynEntities())
            {
                return context.MdaGovernmentMappings.Where(m => m.Government.governmentLevelID == govtLevelId).OrderBy(m => m.mdaName).ToList();
            }
        }

        public List<Government> GetGovernmentsByLevel(int levelId)
        {
            using (var context = new SsepsIISynEntities())
            {
                return context.Governments.Where(g => g.governmentLevelID == levelId).OrderBy(g => g.governmentName).ToList();
            }
        }

        public List<MdaGovernmentMapping> GetMDAsByGovernmentForAssignment(int govtId, int systemUserId)
        {
            using (var context = new SsepsIISynEntities())
            {
                var assignedMDAs = context.SystemUserMdas.Where(s => s.SystemUserId == systemUserId).Select(s => s.MdaId).ToList();
                var supervisedMDAs = context.SystemUserSupervisedMdas.Where(s => s.SystemUserId == systemUserId).Select(s => s.MdaId).ToList();
                var mdas = context.MdaGovernmentMappings.Where(m => m.governmentID == govtId).OrderBy(m => m.mdaName).ToList();
                foreach (var mda in mdas)
                {
                    if (assignedMDAs.Contains(mda.mdaID))
                        mda.IsAssigned = true;
                    if (supervisedMDAs.Contains(mda.mdaID))
                        mda.IsSupervised = true;
                }
                return mdas;
            }
        }

        public List<MdaStructure> GetMdaStructures()
        {
            using (var context = new SsepsIISynEntities())
            {
                return context.MdaStructures.OrderBy(m => m.mdaStructureName).ToList();
            }
        }

        public MdaStructure GetMdaStructureById(int mdaStructureId)
        {
            using (var context = new SsepsIISynEntities())
            {
                return context.MdaStructures.FirstOrDefault(m => m.mdaStructureID == mdaStructureId);
            }
        }

        public Government GetGovernmentById(int governmentId)
        {
            using (var context = new SsepsIISynEntities())
            {
                return context.Governments.FirstOrDefault(g => g.governmentID == governmentId);
            }
        }

        public List<DirectorateListing> GetMdaDirectorates(List<int> mdaIds)
        {
            using (var context = new SsepsIISynEntities())
            {
                return context.DirectorateListings.Include("MdaGovernmentMapping").Where(d => mdaIds.Contains(d.mdaID)).OrderBy(d => d.directorateName).ToList();
            }
        }
    }
}