using System;

namespace Gamgaroo.Esmeralda.Core.Models
{
    public sealed class BuildStatusModel
    {
        public int Build { get; set; }
        public string Buildtargetid { get; set; }
        public string BuildTargetName { get; set; }
        public string BuildStatus { get; set; }
        public string Platform { get; set; }
        public int WorkspaceSize { get; set; }
        public DateTime Created { get; set; }
        public DateTime Finished { get; set; }
        public DateTime CheckoutStartTime { get; set; }
        public int CheckoutTimeInSeconds { get; set; }
        public DateTime BuildStartTime { get; set; }
        public float BuildTimeInSeconds { get; set; }
        public DateTime PublishStartTime { get; set; }
        public float PublishTimeInSeconds { get; set; }
        public float TotalTimeInSeconds { get; set; }
        public string LastBuiltRevision { get; set; }
        public string[] Changeset { get; set; }
        public bool Favorited { get; set; }
        public int AuditChanges { get; set; }
        public ProjectVersion ProjectVersion { get; set; }
        public BuildStatusModelLinks Links { get; set; }
    }
}