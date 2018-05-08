using System.Threading.Tasks;
using Gamgaroo.Esmeralda.Core.Models;

namespace Gamgaroo.Esmeralda.Core.Services
{
    public interface IBuildService
    {
        Task HandleAsync(WebHookModel model, string wwwroot);
    }
}