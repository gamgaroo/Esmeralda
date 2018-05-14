using System.Threading.Tasks;

namespace Gamgaroo.Esmeralda.Integrations.Slack.Services
{
    public interface ISlackClient
    {
        Task PostStartedAsync(string projectName, string buildTargetName, int buildNumber, string startedBy);
        Task PostSuccessAsync(string projectName, string buildTargetName, int buildNumber, string startedBy);
        Task PostFailureAsync(string projectName, string buildTargetName, int buildNumber, string startedBy);
    }
}