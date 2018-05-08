using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Gamgaroo.Esmeralda.Core.Models;
using Newtonsoft.Json;

namespace Gamgaroo.Esmeralda.Core.Services
{
    public sealed class BuildStatusService : IBuildStatusService
    {
        public async Task<BuildStatusModel> GetBuildStatusModelAsync(string url, string apiKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Basic {apiKey}");

                using (var result = await client.GetAsync(url))
                {
                    if (result.IsSuccessStatusCode)
                        return JsonConvert.DeserializeObject<BuildStatusModel>(
                            await result.Content.ReadAsStringAsync());

                    throw new WebException(result.StatusCode.ToString());
                }
            }
        }
    }
}