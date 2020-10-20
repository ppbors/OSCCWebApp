using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class FTranslations
    {
        public int Id { get; set; }
        public int? Fragment { get; set; }
        public string Translation { get; set; }

        public virtual Fragments FragmentNavigation { get; set; }
    }
}
