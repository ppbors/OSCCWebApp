﻿using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class FCommentary
    {
        public int Id { get; set; }
        public int? Fragment { get; set; }
        public string Commentary { get; set; }

        public virtual Fragments FragmentNavigation { get; set; }
    }
}
