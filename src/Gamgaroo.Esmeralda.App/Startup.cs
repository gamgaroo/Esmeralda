using Gamgaroo.Esmeralda.App.Extensions;
using Gamgaroo.Esmeralda.Core.Extensions;
using Gamgaroo.Esmeralda.Integrations.Slack.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gamgaroo.Esmeralda.App
{
    public sealed class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAdminCredentials(_configuration.GetSection("admin"));
            services.AddEsmeraldaCore(_configuration.GetSection("unity"));
            services.AddSlackIntegration(_configuration.GetSection("slack"));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles(CreateStaticFilesOptions());

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }

        // Unity3D WebGL MIME Type support
        private static StaticFileOptions CreateStaticFilesOptions()
        {
            var options = new StaticFileOptions();
            var provider = new FileExtensionContentTypeProvider();

            provider.Mappings.Add(".unityweb", "application/octet-stream");

            options.ContentTypeProvider = provider;

            return options;
        }
    }
}