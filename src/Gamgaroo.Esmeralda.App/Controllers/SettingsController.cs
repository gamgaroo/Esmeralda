using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Gamgaroo.Esmeralda.App.Attributes;
using Gamgaroo.Esmeralda.App.ViewModels;
using Gamgaroo.Esmeralda.Core.Options;
using Gamgaroo.Esmeralda.Integrations.Slack.Options;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gamgaroo.Esmeralda.App.Controllers
{
    [BasicAuthorization]
    [Route("api/settings")]
    public sealed class SettingsController : Controller
    {
        private readonly SlackOptions _slackOptions;
        private readonly UnityOptions _unityOptions;

        public SettingsController(SlackOptions slackOptions, UnityOptions unityOptions)
        {
            _slackOptions = slackOptions;
            _unityOptions = unityOptions;
        }

        private static string SettingsPath => Path.Combine(Directory.GetCurrentDirectory(), "Settings");

        [HttpGet]
        public IActionResult Get()
        {
            var settings = new SettingsViewModel
            {
                Slack = _slackOptions,
                Unity = _unityOptions
            };

            return Json(settings);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SettingsViewModel body)
        {
            await WriteSettings("unity", body.Unity);
            await WriteSettings("slack", body.Slack);

            return Ok();
        }

        private static async Task WriteSettings(string name, object obj)
        {
            var path = Path.Combine(SettingsPath, $"{name}.json");
            var dict = new Dictionary<string, object> {{name, obj}};

            using (var file = System.IO.File.CreateText(path))
            {
                await file.WriteAsync(JsonConvert.SerializeObject(dict, Formatting.Indented));
            }
        }
    }
}