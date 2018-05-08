using System;
using System.Linq;
using System.Threading.Tasks;
using Gamgaroo.Esmeralda.Core.Models;
using Gamgaroo.Esmeralda.Core.Services;
using Gamgaroo.Esmeralda.Integrations.Slack;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gamgaroo.Esmeralda.App.Controllers
{
    [Route("api/builds")]
    public sealed class BuildsController : Controller
    {
        private readonly IBuildService _buildService;
        private readonly IHostingEnvironment _environment;
        private readonly ILogger<BuildsController> _logger;
        private readonly ISlackClient _slackClient;

        public BuildsController(
            ILogger<BuildsController> logger,
            IHostingEnvironment environment,
            IBuildService buildService,
            ISlackClient slackClient)
        {
            _logger = logger;
            _environment = environment;
            _buildService = buildService;
            _slackClient = slackClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WebHookModel model)
        {
            var unityCloudBuildEvent = GetUnityCloudBuildEventHeaderValue();

            if (unityCloudBuildEvent == null)
                return BadRequest();

            if (unityCloudBuildEvent != "ProjectBuildSuccess")
                return Ok();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _slackClient.PostStartedAsync(
                    model.ProjectName,
                    model.BuildTargetName,
                    model.BuildNumber,
                    model.StartedBy);

                await _buildService.HandleAsync(model, _environment.WebRootPath);

                await _slackClient.PostSuccessAsync(
                    model.ProjectName,
                    model.BuildTargetName,
                    model.BuildNumber,
                    model.StartedBy);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());

                await _slackClient.PostFailureAsync(
                    model.ProjectName,
                    model.BuildTargetName,
                    model.BuildNumber,
                    model.StartedBy);

                throw;
            }

            return Ok();
        }

        private string GetUnityCloudBuildEventHeaderValue()
        {
            return Request.Headers["X-Unitycloudbuild-Event"].FirstOrDefault();
        }
    }
}