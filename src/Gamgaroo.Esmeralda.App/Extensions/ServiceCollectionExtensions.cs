using Gamgaroo.Esmeralda.App.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Gamgaroo.Esmeralda.App.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdminCredentials(this IServiceCollection services,
            IConfigurationSection adminSection)
        {
            services.Configure<AdminCredentials>(adminSection);
            services.AddScoped(p => p.GetService<IOptionsSnapshot<AdminCredentials>>().Value);

            return services;
        }
    }
}