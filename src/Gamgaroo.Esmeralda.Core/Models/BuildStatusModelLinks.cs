namespace Gamgaroo.Esmeralda.Core.Models
{
    public sealed class BuildStatusModelLinks
    {
        public Link Self { get; set; }
        public Link Log { get; set; }
        public Link Auditlog { get; set; }
        public Link Download_Primary { get; set; }
        public Link Create_Share { get; set; }
        public Link Revoke_Share { get; set; }
        public Link Icon { get; set; }
    }
}