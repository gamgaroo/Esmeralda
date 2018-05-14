using Gamgaroo.Esmeralda.Core.Options;
using Gamgaroo.Esmeralda.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Gamgaroo.Esmeralda.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEsmeraldaCore(this IServiceCollection services,
            IConfigurationSection unitySection)
        {
            services.AddTransient<IBuildService, BuildService>();
            services.AddTransient<IBuildStatusService, BuildStatusService>();
            services.AddTransient<IDownloadService, DownloadService>();

            services.Configure<UnityOptions>(unitySection);
            services.AddScoped(p => p.GetService<IOptionsSnapshot<UnityOptions>>().Value);

            return services;
        }
    }
}