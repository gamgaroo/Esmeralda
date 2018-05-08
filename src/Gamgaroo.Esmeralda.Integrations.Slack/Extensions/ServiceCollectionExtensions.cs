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

            services.Configure<SlackClientOptions>(slackSection);
            services.AddScoped(p => p.GetService<IOptionsSnapshot<SlackClientOptions>>().Value);

            return services;
        }
    }
}