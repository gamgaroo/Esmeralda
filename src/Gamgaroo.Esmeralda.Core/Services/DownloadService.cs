using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gamgaroo.Esmeralda.Core.Services
{
    public sealed class DownloadService : IDownloadService
    {
        public async Task<Stream> DownloadAsStreamAsync(string url)
        {
            using (var client = new HttpClient())
            {
                using (var result = await client.GetAsync(url))
                {
                    if (result.IsSuccessStatusCode)
                        return new MemoryStream(await result.Content.ReadAsByteArrayAsync());

                    throw new WebException(result.StatusCode.ToString());
                }
            }
        }
    }
}