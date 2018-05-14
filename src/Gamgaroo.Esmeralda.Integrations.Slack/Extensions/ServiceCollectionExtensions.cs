using Gamgaroo.Esmeralda.Integrations.Slack.Options;
using Gamgaroo.Esmeralda.Integrations.Slack.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Gamgaroo.Esmeralda.Integrations.Slack.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSlackIntegration(this IServiceCollection services,
            IConfigurationSection slackSection)
        {
            services.AddTransient<ISlackClient, SlackClient>();

            services.Configure<SlackOptions>(slackSection);
            services.AddScoped(p => p.GetService<IOptionsSnapshot<SlackOptions>>().Value);

            return services;
        }
    }
}