using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SmartCart.Application.Contracts;
using SmartCart.Application.Dtos;
using SmartCart.Core.Constants;
using SmartCart.Core.Entities;
using SmartCart.Core.Exceptions;
using SmartCart.Infrastructure.Constants;

namespace SmartCart.Infrastructure.Services
{
    public class AuthenticationService(ILogger<AuthenticationService> logger,
        UserManager<User> userManager) : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger = logger;
        private readonly UserManager<User> _userManager = userManager;

        public async Task<UserResponseDto> RegisterCustomer(RegisterUserDto registerUserDto)
        {
            try
            {
                var userInDb = await _userManager.FindByEmailAsync(registerUserDto.Email);
                if (userInDb != null)
                {
                    throw new UserAlreadyExistsException(UserConstants.CustomerAlreadyExists);
                }

                var user = new User
                {
                    Email = registerUserDto.Email,
                    PhoneNumber = registerUserDto.PhoneNumber,
                    UserName = registerUserDto.UserName
                };

                var userCreated = await _userManager.CreateAsync(user, registerUserDto.Password);

                if (userCreated != null && userCreated.Errors.Count() > 0)
                {
                    var topError = userCreated.Errors.FirstOrDefault();
                    throw new Exception(topError.Description);
                }

                if (userCreated.Succeeded)
                {
                    // assign customer role
                    await _userManager.AddToRoleAsync(user, RoleConstants.Customer);

                    var response = new UserResponseDto{ };
                    return response;
                }

                throw new Exception(CommonConstants.SomethingWentWrong);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
