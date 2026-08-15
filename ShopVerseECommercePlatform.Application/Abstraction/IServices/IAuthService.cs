using ShopVerseECommercePlatform.Application.RRModels.Auth;
using ShopVerseECommercePlatform.Application.Utils.Result;
using System;
using System.Collections.Generic;
using System.Text;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Application.Abstraction.IServices
{
    public interface IAuthService
    {
        Task<Result<string>> SignUpAsync(SignupRequest model);
        Task<Result<string>> CreateAdminAsync(SignupRequest model);
        Task<Result<string>> LoginAsync(LoginRequest model);
        Task<Result<string>> ChangePassword(ChangePassword model);

    }
}
