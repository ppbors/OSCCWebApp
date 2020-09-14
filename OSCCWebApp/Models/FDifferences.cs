using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class FDifferences
    {
        public int Id { get; set; }
        public int? Fragment { get; set; }
        public string Differences { get; set; }

        public virtual FragmentReferencer FragmentNavigation { get; set; }
    }
}
