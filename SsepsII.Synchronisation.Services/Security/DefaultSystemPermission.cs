using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Dal;

namespace SsepsII.Services.Security
{
    public class DefaultSystemPermission
    {
        public DefaultSystemPermission()
        {
            this.SystemPermissions = new List<SystemPermission>();
        }

        /// <summary>
        /// Gets or sets the role system name
        /// </summary>
        public string RoleSystemName { get; set; }

        /// <summary>
        /// Gets or sets the permissions
        /// </summary>
        public IEnumerable<SystemPermission> SystemPermissions { get; set; }
    }
}
