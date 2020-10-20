using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class Fragments
    {
        public Fragments()
        {
            FContext = new HashSet<FContext>();
        }

        public int Id { get; set; }
        public int Book { get; set; }
        public int Editor { get; set; }
        public string FragmentName { get; set; }
        public string LineName { get; set; }
        public string LineContent { get; set; }
        public int? Published { get; set; }
        public string Status { get; set; }

        public virtual Books BookNavigation { get; set; }
        public virtual Editors EditorNavigation { get; set; }
        public virtual FApparatus FApparatus { get; set; }
        public virtual FCommentary FCommentary { get; set; }
        public virtual FDifferences FDifferences { get; set; }
        public virtual FReconstruction FReconstruction { get; set; }
        public virtual FTranslations FTranslations { get; set; }
        public virtual ICollection<FContext> FContext { get; set; }
    }
}
