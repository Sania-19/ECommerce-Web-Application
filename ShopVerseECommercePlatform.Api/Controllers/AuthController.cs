using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopVerseECommercePlatform.Api.CustomExtensions;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using ShopVerseECommercePlatform.Application.RRModels.Auth;
using ShopVerseECommercePlatform.Application.Services;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("signup")]
        public async Task<IResult> SignUp(SignupRequest model)
        {
            return this.ApiResponse(await authService.SignUpAsync(model));
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("admin")]
        public async Task<IResult> CreateAdmin([FromForm] SignupRequest model) => this.ApiResponse(await authService.CreateAdminAsync(model));

        [HttpPost("login")]
        public async Task<IResult> Login(LoginRequest model) => this.ApiResponse(await authService.LoginAsync(model));
    }
}
