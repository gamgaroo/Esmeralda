using System.IO;
using System.Threading.Tasks;

namespace Gamgaroo.Esmeralda.Core.Services
{
    public interface IDownloadService
    {
        Task<Stream> DownloadAsStreamAsync(string url);
    }
}