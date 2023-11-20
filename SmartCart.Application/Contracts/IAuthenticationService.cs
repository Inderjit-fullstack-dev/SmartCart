using SmartCart.Application.Dtos;

namespace SmartCart.Application.Contracts
{
    public interface IAuthenticationService
    {
        Task<UserResponseDto> RegisterCustomer(RegisterUserDto registerUserDto);
    }
}
