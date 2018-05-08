using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Gamgaroo.Esmeralda.Integrations.Slack
{
    public sealed class SlackClient : ISlackClient
    {
        private readonly SlackClientOptions _options;

        public SlackClient(
            SlackClientOptions options)
        {
            _options = options;
        }

        public async Task PostStartedAsync(
            string projectName,
            string buildTargetName,
            int buildNumber,
            string startedBy)
        {
            var payload = new Payload
            {
                Attachments = new[]
                {
                    new Attachment
                    {
                        Color = Colors.Info,
                        Title = "Deployment Started",
                        Fields = GetFields(projectName, buildTargetName, buildNumber, startedBy)
                    }
                }
            };

            await SendAsync(payload);
        }

        public async Task PostSuccessAsync(
            string projectName,
            string buildTargetName,
            int buildNumber,
            string startedBy)
        {
            var payload = new Payload
            {
                Attachments = new[]
                {
                    new Attachment
                    {
                        Color = Colors.Success,
                        Title = "Deployment Success",
                        Fields = GetFields(projectName, buildTargetName, buildNumber, startedBy)
                    }
                }
            };

            await SendAsync(payload);
        }

        public async Task PostFailureAsync(
            string projectName,
            string buildTargetName,
            int buildNumber,
            string startedBy)
        {
            var payload = new Payload
            {
                Attachments = new[]
                {
                    new Attachment
                    {
                        Color = Colors.Failure,
                        Title = "Deployment Failure",
                        Fields = GetFields(projectName, buildTargetName, buildNumber, startedBy)
                    }
                }
            };

            await SendAsync(payload);
        }

        private static Attachment.Field[] GetFields(
            string projectName,
            string buildTargetName,
            int buildNumber,
            string startedBy)
        {
            return new[]
            {
                new Attachment.Field("Project", projectName),
                new Attachment.Field("Build Target", buildTargetName),
                new Attachment.Field("Build Number", buildNumber),
                new Attachment.Field("Started By", startedBy)
            };
        }

        private async Task SendAsync(Payload payload)
        {
            if (!_options.Enable)
                return;

            using (var client = new HttpClient())
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var str = JsonConvert.SerializeObject(payload, settings);
                var content = new StringContent(str);
                await client.PostAsync(_options.WebhookUrl, content);
            }
        }
    }
}