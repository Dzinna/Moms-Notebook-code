using System.Threading.Tasks;

namespace MomsNotebook.Services
{
    public interface ISynchronizationService
    {
        Task Synchronize();
        Task UpdateInfoTable();
    }
}
