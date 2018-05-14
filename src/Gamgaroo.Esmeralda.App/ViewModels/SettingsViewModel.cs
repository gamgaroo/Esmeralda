using Gamgaroo.Esmeralda.Core.Options;
using Gamgaroo.Esmeralda.Integrations.Slack.Options;

namespace Gamgaroo.Esmeralda.App.ViewModels
{
    public sealed class SettingsViewModel
    {
        public UnityOptions Unity { get; set; }
        public SlackOptions Slack { get; set; }
    }
}