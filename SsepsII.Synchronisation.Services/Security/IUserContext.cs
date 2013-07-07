using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SsepsII.Dal;

namespace SsepsII.Services.Security
{
    public interface IUserContext
    {
        SystemUser CurrentUser { get; set; }

        //T Resolve<T>();

        bool IsAuthenticated { get; set; }

        bool HasPermission(SystemPermission permissionRecord);
        bool HasPermission(String permissionName);
    }
}
