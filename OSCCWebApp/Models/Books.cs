using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class Books
    {
        public Books()
        {
            Bibliography = new HashSet<Bibliography>();
            Editors = new HashSet<Editors>();
            Fragments = new HashSet<Fragments>();
            TCommentary = new HashSet<TCommentary>();
            TLines = new HashSet<TLines>();
        }

        public int Id { get; set; }
        public int Author { get; set; }
        public string Title { get; set; }

        public virtual Authors AuthorNavigation { get; set; }
        public virtual ICollection<Bibliography> Bibliography { get; set; }
        public virtual ICollection<Editors> Editors { get; set; }
        public virtual ICollection<Fragments> Fragments { get; set; }
        public virtual ICollection<TCommentary> TCommentary { get; set; }
        public virtual ICollection<TLines> TLines { get; set; }
    }
}
