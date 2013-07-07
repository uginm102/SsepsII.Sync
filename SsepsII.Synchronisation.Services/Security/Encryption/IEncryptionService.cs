using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsepsII.Synchronisation.Services
{
    public interface IEncryptionService
    {
        string CreateSaltKey(int size);
        String GetSHA256(String value, String salt, bool useSystemSalt);
    }
}
