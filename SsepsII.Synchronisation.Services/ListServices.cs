using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Synchronisation.DAL;
using System.Globalization;

namespace SsepsII.Synchronisation.Services
{
    public class Year
    {
        public int yearID { set; get; }
        public string yearName { set; get; }
    }
    

    public class ListServices
    {
        

        public List<ListItem> State
        {
            get
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    return ents.ListItems.Where(x => x.listTypeID == 5).OrderBy(x => x.listText).ToList();
                }
            }
        }

        public List<State> StateList
        {
            get
            {
                using (var context = new SsepsIISynEntities())
                {
                    return context.States.OrderBy(s => s.StateName).ToList();
                }
            }
        }

        
        public List<ListItem> GovernmentLevel
        {
            get
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    return ents.ListItems.Where(x => x.listTypeID == 8).ToList();
                }
            }
        }

        public List<Government> Governments
        {
            get
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    return ents.Governments.ToList();
                }
            }
        }

        public List<MdaStructure> MdaStructures
        {
            get
            {
                using (var context = new SsepsIISynEntities())
                {
                    return context.MdaStructures.OrderBy(m => m.mdaStructureName).ToList();
                }
            }
        }

        
        public static string GetListTextById(string listId)
        {
            int id;
            return int.TryParse(listId, out id) ? GetListTextById(int.Parse(listId)) : string.Empty;
        }

        public static string GetListTextById(int listId)
        {
            using (var context = new SsepsIISynEntities())
            {
                var listItem = context.ListItems.FirstOrDefault(l => l.listID == listId);
                if (listItem != null)
                    return listItem.listText;
                else
                    return string.Empty;
            }
        }

        public List<ListItem> GetGovernmentLevels()
        {
            return GovernmentLevel;
        }
    }
}
