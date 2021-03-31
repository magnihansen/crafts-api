using System.Threading;
using System.Threading.Tasks;

namespace CraftsApi.Service.Background
{
    public interface IPageWorker
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
