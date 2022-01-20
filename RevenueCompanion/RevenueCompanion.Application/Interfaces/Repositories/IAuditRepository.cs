using System.Threading.Tasks;

namespace RevenueCompanion.Application.Interfaces.Repositories
{
    public interface IAuditRepository
    {
        Task CreateAudit(string userId, string action);
    }
}
