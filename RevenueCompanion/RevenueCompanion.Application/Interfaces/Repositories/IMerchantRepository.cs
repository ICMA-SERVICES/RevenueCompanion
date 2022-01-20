using RevenueCompanion.Application.DTOs;
using RevenueCompanion.Application.DTOs.Merchant;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Interfaces.Repositories
{
    public interface IMerchantRepository
    {
        Task<RepositoryResponse> CreateMerchantAsync(CreateMerchantDTO request);
        Task<MerchantDTO> GetFirstMerchant();
    }
}
