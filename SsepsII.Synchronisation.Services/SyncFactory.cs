using SsepsII.Synchronisation.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SsepsII.Synchronisation.Services
{
    public static class SyncFactory
    {
        public static SyncData GetSyncObject(List<TransData> relatedTables)
        {
            return new SyncData(relatedTables);
        }
    }
}
