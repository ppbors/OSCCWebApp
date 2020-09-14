using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class Comments2
    {
        public int Id { get; set; }
        public int Text { get; set; }
        public int? LineStart { get; set; }
        public int? LineEnd { get; set; }
        public string LineCommentaar { get; set; }

        public virtual Text TextNavigation { get; set; }
    }
}
