using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class TLines
    {
        public int Id { get; set; }
        public int Book { get; set; }
        public int? LineNumber { get; set; }
        public string LineContent { get; set; }

        public virtual Books BookNavigation { get; set; }
    }
}
