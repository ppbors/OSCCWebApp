using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class FContext
    {
        public int Id { get; set; }
        public int? Fragment { get; set; }
        public string ContextAuthor { get; set; }
        public string Context { get; set; }

        public virtual FragmentReferencer FragmentNavigation { get; set; }
    }
}
