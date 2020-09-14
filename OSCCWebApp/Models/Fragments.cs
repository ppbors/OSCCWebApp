using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class Fragments
    {
        public int Id { get; set; }
        public int Book { get; set; }
        public string FragmentName { get; set; }
        public string LineName { get; set; }
        public int Editor { get; set; }
        public string FragmentContent { get; set; }
        public int? Published { get; set; }
        public string Status { get; set; }

        public virtual Books BookNavigation { get; set; }
    }
}
