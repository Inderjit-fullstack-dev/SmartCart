using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartCart.Application.Contracts;
using SmartCart.Application.Dtos;
using SmartCart.Core.Constants;
using SmartCart.Core.Database;
using SmartCart.Core.Entities;
using SmartCart.Core.Exceptions;
using SmartCart.Infrastructure.Constants;

namespace SmartCart.Infrastructure.Services
{
    public class AuthenticationService(ILogger<AuthenticationService> logger,
        UserManager<User> userManager, IJWTService jwtService, ApplicationDBContext dbContext) : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger = logger;
        private readonly UserManager<User> _userManager = userManager;
        private readonly IJWTService _jwtService = jwtService;
        private readonly ApplicationDBContext _dbContext = dbContext;

        public async Task<UserResponseDto> RegisterCustomer(RegisterUserDto registerUserDto)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
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

                if (userCreated != null && userCreated.Errors.Any())
                {
                    var topError = userCreated.Errors.FirstOrDefault();
                    throw new Exception(topError.Description);
                }

                if (userCreated.Succeeded)
                {
                    // Assign customer role
                    await _userManager.AddToRoleAsync(user, RoleConstants.Customer);

                    var newUser = await _userManager.FindByEmailAsync(user.Email);

                    // Generate token using newUser.Id
                    var token = await _jwtService.GenerateJwtToken(newUser);

                    var response = new UserResponseDto
                    {
                        Id = newUser.Id,
                        UserName = newUser.UserName,
                        Email = newUser.Email,
                        EmailConfirmed = newUser.EmailConfirmed,
                        PhoneNumber = newUser.PhoneNumber,
                        PhoneNumberConfirmed = newUser.PhoneNumberConfirmed,
                        Token = token,
                    };

                    // Commit the transaction since everything succeeded
                    transaction.Commit();

                    return response;
                }

                throw new Exception(CommonConstants.SomethingWentWrong);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, ex.Message);

                // Rollback the transaction in case of an exception
                transaction.Rollback();
                throw;
            }
        }

    }
}
