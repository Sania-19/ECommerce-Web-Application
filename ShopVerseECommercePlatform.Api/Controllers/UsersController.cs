using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopVerseECommercePlatform.Api.CustomExtensions;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IResult> GetUsers()
        {
            return this.ApiResponse(await userService.GetUsers());
        }

        [HttpGet("email")]
        public async Task<IResult> GetUserByEmail(string email) => this.ApiResponse(await userService.GetUserByEmail(email));

        [HttpGet("by-roles")]
        public async Task<IResult> GetUsersByRoles([FromQuery] IEnumerable<string> userRoles)
        {
            return this.ApiResponse(await userService.GetUsersByRoles(userRoles));
        }

        [HttpGet("{id:guid}")]
        public async Task<IResult> GetUserById(Guid id)
        {
            return this.ApiResponse(await userService.GetUserById(id));
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IResult> UpdateUserStatus(UserStatus userStatus, Guid id)
        {
            return this.ApiResponse(await userService.UpdateUserStatus(userStatus, id));
        }
    }
}
