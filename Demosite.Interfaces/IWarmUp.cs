using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Interfaces
{
    public interface IWarmUp
    {
        Task WarmpUpEmail(CancellationToken cancellationToken);
    }
}
