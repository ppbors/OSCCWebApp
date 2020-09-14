using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class Bibliography
    {
        public int Id { get; set; }
        public int? Text { get; set; }
        public string Editors { get; set; }
        public string Author { get; set; }
        public string Book { get; set; }
        public string Article { get; set; }
        public string Journal { get; set; }
        public string Volume { get; set; }
        public string ChapterTitle { get; set; }
        public string Pages { get; set; }
        public string Place { get; set; }
        public string Year { get; set; }
        public string Website { get; set; }
        public string Url { get; set; }
        public string ConsultDate { get; set; }

        public virtual Books TextNavigation { get; set; }
    }
}
