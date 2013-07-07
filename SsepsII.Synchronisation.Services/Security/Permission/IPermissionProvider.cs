using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Dal;

namespace SsepsII.Services.Security
{
    public interface IPermissionProvider
    {
        IEnumerable<SystemPermission> GetPermissions();

        IEnumerable<DefaultSystemPermission> GetDefaultPermissions();
    }
}
