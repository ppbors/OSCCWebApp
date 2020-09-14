using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class Editors
    {
        public int Id { get; set; }
        public int Book { get; set; }
        public string EditorName { get; set; }
        public int? DefaultEditor { get; set; }

        public virtual Books BookNavigation { get; set; }
    }
}
