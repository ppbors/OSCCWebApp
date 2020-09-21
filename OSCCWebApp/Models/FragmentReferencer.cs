using System;
using System.Collections.Generic;

namespace OSCCWebApp
{
    public partial class FragmentReferencer
    {
        public FragmentReferencer()
        {
            FContext = new HashSet<FContext>();
            FReconstruction = new HashSet<FReconstruction>();
        }

        public int Id { get; set; }
        public int Book { get; set; }
        public int Editor { get; set; }
        public int FragmentNo { get; set; }
        public int? Published { get; set; }

        public virtual FApparatus FApparatus { get; set; }
        public virtual FCommentary FCommentary { get; set; }
        public virtual FDifferences FDifferences { get; set; }
        public virtual FTranslations FTranslations { get; set; }
        public virtual ICollection<FContext> FContext { get; set; }
        public virtual ICollection<FReconstruction> FReconstruction { get; set; }
    }
}
