using SsepsII.Synchronisation.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsepsII.Synchronisation.Services
{
    public class InstallationServices
    {
        public List<int> ManagedMDAListBySiteId(int siteId)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                string siteIdValue = siteId.ToString();
                return ents.InstallationConfigs.Where(x =>
                    x.installation_key == Constant.MANAGED_SITE &&
                    x.installation_value == siteIdValue).Select(o => o.mdaId).ToList();
            }
        }

        public string InstallationTokenBySiteId(int siteId)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                string siteIdValue = siteId.ToString();

                if (ents.InstallationConfigs.Where(x => x.installation_key == Constant.MANAGED_SITE && x.installation_value == siteIdValue).Count() > 0)
                {

                    return ents.InstallationConfigs.First(x =>
                        x.installation_key == Constant.MANAGED_SITE &&
                        x.installation_value == siteIdValue).installation_token;
                }
                else return string.Empty;
            }
        }

        public string InstallationTokenByMDAId(int mdaId)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {

                if (ents.InstallationConfigs.Where(x => x.installation_key == Constant.MANAGED_SITE && x.mdaId == mdaId).Count() > 0)
                {

                    return ents.InstallationConfigs.First(x =>
                        x.installation_key == Constant.MANAGED_SITE &&
                        x.mdaId == mdaId).installation_token;
                }
                else return string.Empty;
            }
        }

        public bool MdaIdsBelongToSameInstallation(List<int> mdaIds)
        {
            if (mdaIds == null || mdaIds.Count == 0) return false;

            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {
                return ents.InstallationConfigs.Where(x => x.installation_key == Constant.MANAGED_SITE &&
                    mdaIds.Contains(x.mdaId)).Select(o => o.installation_value).Distinct().Count() == 1 ? true : false;
            }
        }

        public int GetSiteIdFromMDAIdOnServer(int mdaId)
        {
            using (SsepsIISynEntities ents = new SsepsIISynEntities())
            {

                if (ents.InstallationConfigs.Where(x => x.installation_key == Constant.MANAGED_SITE && x.mdaId == mdaId).Count() > 0)
                {
                    string installationValue = ents.InstallationConfigs.First(x => x.installation_key == Constant.MANAGED_SITE && x.mdaId == mdaId).installation_value;
                    int siteId;
                    return int.TryParse(installationValue, out siteId) ? siteId : 0;
                }
                else return 0;
            }
        }
    }
}
