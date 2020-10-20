using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class Editors
    {
        public Editors()
        {
            Fragments = new HashSet<Fragments>();
        }

        public int Id { get; set; }
        public int Book { get; set; }
        public string Name { get; set; }
        public byte? MainEditor { get; set; }

        public virtual Books BookNavigation { get; set; }
        public virtual ICollection<Fragments> Fragments { get; set; }
    }
}
