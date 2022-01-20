namespace RevenueCompanion.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        string MerchantCode { get; }
        string Email { get; }
        string Name { get; }
    }
}
