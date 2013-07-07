using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SsepsII.Synchronisation.DAL
{
    public partial class MdaGovernmentMapping
    {
        public bool IsAssigned { get; set; }

        public bool IsSupervised { get; set; }
    }

    public partial class DirectorateListing
    {
        public bool IsAssigned { get; set; }
    }
}
