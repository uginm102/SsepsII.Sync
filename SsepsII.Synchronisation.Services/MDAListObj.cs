using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsepsII.Synchronisation.Services
{
    public class MDAListObj
    {
        public int id;
        public string Name { get; set; }
        public bool Selected { get; set; }

        public override string ToString()
        {
            return string.Format("{0} | {1} | {2}", id, Name, Selected);
        }
    }
}
