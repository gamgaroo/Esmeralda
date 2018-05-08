using System;

namespace Gamgaroo.Esmeralda.Core.Models
{
    public sealed class ProjectVersion
    {
        public string Name { get; set; }
        public string Filename { get; set; }
        public string ProjectName { get; set; }
        public string Platform { get; set; }
        public int Size { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastMod { get; set; }
        public string BundleId { get; set; }
        public string[] Udids { get; set; }
    }
}