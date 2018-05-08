using System.Threading.Tasks;
using Gamgaroo.Esmeralda.Core.Models;

namespace Gamgaroo.Esmeralda.Core.Services
{
    public interface IBuildStatusService
    {
        Task<BuildStatusModel> GetBuildStatusModelAsync(string url, string apiKey);
    }
}