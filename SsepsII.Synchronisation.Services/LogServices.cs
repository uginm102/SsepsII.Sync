using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Synchronisation.DAL;

namespace SsepsII.Synchronisation.Services
{
    /// <summary>
    /// Levels
    /// 1 = An exception has occured
    /// 2 = Logging critical system information
    /// 3 = Logging traces eg when a sync has failed
    /// 4 = debuging
    /// 
    /// default is 3.
    /// </summary>
    public class LogServices
    {
        private static void Log(string message, string username, int level = 3, string exception = null)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                Log log = new Log();
                log.Message = message;
                log.Level = level;
                log.Exception = exception;
                log.WhoCreated = username;
                log.DateCreated = DateTime.Now;

                ents.Logs.Add(log);
                ents.SaveChanges();
            }
        }

        public static void Log(string callingObject, string callingMethode, string message, string username, int level = 3, string exception = null)
        {
            Log(string.Format("{0} - {1} - {2}", callingObject, callingMethode, message), username, level, exception);
        }

        public static void Log(string callingObject,string callingMethode,string message, SystemUser user, int level = 3, string exception = null)
        {
            Log(callingObject,callingMethode, message, user.Username, level, exception);
        }
    }
}
