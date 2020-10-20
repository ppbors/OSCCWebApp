using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class FReconstruction
    {
        public int Id { get; set; }
        public int? Fragment { get; set; }
        public string Reconstruction { get; set; }

        public virtual Fragments FragmentNavigation { get; set; }
    }
}
