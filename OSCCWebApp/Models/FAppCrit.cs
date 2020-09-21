using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class FApparatus
    {
        public int Id { get; set; }
        public int? Fragment { get; set; }
        public string Apparatus { get; set; }

        public virtual FragmentReferencer FragmentNavigation { get; set; }
    }
}
