using SmartCart.Core.Entities;

namespace SmartCart.Application.Contracts
{
    public interface IJWTService
    {
        Task<string> GenerateJwtToken(User user);
    }
}
