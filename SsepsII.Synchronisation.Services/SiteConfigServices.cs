using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Synchronisation.DAL;

namespace SsepsII.Synchronisation.Services
{
    public class SiteConfigServices
    {
        public static bool IsServer
        {
            get
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    if (ents.SITE_CONFIG.Where(x => x.CONFIG_KEY == Constant.SITE_TYPE).Count() == 1)
                    {
                        return int.Parse(ents.SITE_CONFIG.Single(x => x.CONFIG_KEY == Constant.SITE_TYPE).CONFIG_VALUE) == (int)SiteType.Server;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static bool IsClient
        {
            get
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    if (ents.SITE_CONFIG.Where(x => x.CONFIG_KEY == Constant.SITE_TYPE).Count() == 1)
                    {
                        return int.Parse(ents.SITE_CONFIG.Single(x => x.CONFIG_KEY == Constant.SITE_TYPE).CONFIG_VALUE) == (int)SiteType.Client;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static int SiteID
        {
            get
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    return int.Parse(ents.SITE_CONFIG.Single(x => x.CONFIG_KEY == Constant.SITE_ID).CONFIG_VALUE);
                }
            }
            set
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    if (ents.SITE_CONFIG.Where(x => x.CONFIG_KEY == Constant.SITE_ID).Count() > 0)
                    {
                        SITE_CONFIG siteConfig = ents.SITE_CONFIG.Single(x => x.CONFIG_KEY == Constant.SITE_ID);
                        siteConfig.CONFIG_VALUE = value.ToString();
                    }
                    else
                    {
                        SITE_CONFIG siteConfig = new SITE_CONFIG();
                        siteConfig.CONFIG_KEY = Constant.SITE_ID;
                        siteConfig.CONFIG_VALUE = value.ToString();

                        ents.SITE_CONFIG.Add(siteConfig);
                    }
                    ents.SaveChanges();
                }
            }
        }
        public static string InstallationToken
        {
            get
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    //TODO work on the single values
                   SITE_CONFIG siteConfig = ents.SITE_CONFIG.SingleOrDefault(x => x.CONFIG_KEY == Constant.INSTALLATION_TOKEN);
                    return siteConfig == null ? string.Empty : siteConfig.CONFIG_VALUE;
                }
            }
            set
            {
                using (SsepsIISynEntities ents = new SsepsIISynEntities())
                {
                    if (ents.SITE_CONFIG.Where(x => x.CONFIG_KEY == Constant.INSTALLATION_TOKEN).Count() > 0)
                    {
                        SITE_CONFIG siteConfig = ents.SITE_CONFIG.Single(x => x.CONFIG_KEY == Constant.INSTALLATION_TOKEN);
                        siteConfig.CONFIG_VALUE = value.ToString();
                    }
                    else
                    {
                        SITE_CONFIG siteConfig = new SITE_CONFIG();
                        siteConfig.CONFIG_KEY = Constant.INSTALLATION_TOKEN;
                        siteConfig.CONFIG_VALUE = value.ToString();

                        ents.SITE_CONFIG.Add(siteConfig);
                    }
                    ents.SaveChanges();
                }
            }
        }
        

        public static bool IsManagedMDA(int mdaId)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                string mdaIdString = mdaId.ToString();
                return ents.SITE_CONFIG.Where(x => x.CONFIG_VALUE == mdaIdString).Count() > 0;
            }
        }

        public static List<int> ManagedMdaIdList()
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                return ents.SITE_CONFIG.Where(x => x.CONFIG_KEY == Constant.SITE_ID).Select(o => int.Parse(o.CONFIG_VALUE)).ToList();
            }
        }
    }
}
