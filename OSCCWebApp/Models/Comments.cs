using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
 

    public partial class Comments
    {
        public int Id { get; set; }
        public int Book { get; set; }
        public int LineStart { get; set; }
        public int LineEnd { get; set; }
        public string RelevantWords { get; set; }
        public string Commentary { get; set; }
        public string Source { get; set; }
        public string Pages { get; set; }

        public virtual TText TextNavigation { get; set; }
    }
}
