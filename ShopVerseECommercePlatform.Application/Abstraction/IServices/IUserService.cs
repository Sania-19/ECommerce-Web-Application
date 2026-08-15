using ShopVerseECommercePlatform.Application.RRModels.Users;
using ShopVerseECommercePlatform.Application.Utils.Result;
using System;
using System.Collections.Generic;
using System.Text;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Application.Abstraction.IServices
{
    public interface IUserService
    {
        Task<Result<IEnumerable<UserResponse>>> GetUsers();
        Task<Result<UserResponse>> GetUserById(Guid id);
        Task<Result<IEnumerable<UserResponse>>> GetUsersByRoles(IEnumerable<string> userRoles);
        Task<Result<IEnumerable<UserResponse>>> GetUserByEmail(string email);
        Task<Result<string>> UpdateUserStatus(UserStatus userStatus, Guid userId);
    }
}
