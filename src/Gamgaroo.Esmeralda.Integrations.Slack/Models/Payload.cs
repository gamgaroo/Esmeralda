using Newtonsoft.Json;

namespace Gamgaroo.Esmeralda.Integrations.Slack
{
    public sealed class Payload
    {
        [JsonProperty("channel")] public string Channel { get; set; }

        [JsonProperty("username")] public string Username { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("Attachments")] public Attachment[] Attachments { get; set; }
    }
}