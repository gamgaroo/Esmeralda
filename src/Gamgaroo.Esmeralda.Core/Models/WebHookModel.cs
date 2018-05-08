namespace Gamgaroo.Esmeralda.Core.Models
{
    public sealed class WebHookModel
    {
        public string ProjectName { get; set; }
        public string BuildTargetName { get; set; }
        public string ProjectGuid { get; set; }
        public string OrgForeignKey { get; set; }
        public int BuildNumber { get; set; }
        public string BuildStatus { get; set; }
        public string StartedBy { get; set; }
        public string Platform { get; set; }
        public WebHookModelLinks Links { get; set; }
    }
}