using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class FAppCrit
    {
        public int Id { get; set; }
        public int? Fragment { get; set; }
        public string AppCrit { get; set; }

        public virtual FragmentReferencer FragmentNavigation { get; set; }
    }
}
