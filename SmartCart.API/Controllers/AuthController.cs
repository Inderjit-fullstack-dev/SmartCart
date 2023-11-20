using Microsoft.AspNetCore.Mvc;
using SmartCart.Application.Contracts;
using SmartCart.Application.Dtos;

namespace SmartCart.API.Controllers
{
    public class AuthController(IAuthenticationService authenticationService) : BaseController
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(RegisterUserDto request)
        {
            var response = await _authenticationService.RegisterCustomer(request);
            return Ok(response);
        }
    }
}
