using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopVerseECommercePlatform.Application.Abstraction.IAppEncryption;
using ShopVerseECommercePlatform.Application.Abstraction.IContextService;
using ShopVerseECommercePlatform.Application.Abstraction.IJWTProvider;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using ShopVerseECommercePlatform.Application.Abstraction.IUnitOfWork;
using ShopVerseECommercePlatform.Application.RRModels.Auth;
using ShopVerseECommercePlatform.Application.Utils.Result;
using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Application.Services
{
    public class AuthService(IAppEncryption appEncryption, IAuthRepository authRepository,
                             IJWTProvider jwtProvider, IContextService contextService, IUnitOfWork unitOfWork) : IAuthService
    {
        private async Task<Result<User>> CreateUserAsync(string email, string phoneNo, string password, UserRole role)
        {
            var isExist = await authRepository.IsExistAsync(
                x => x.Email == email
            );

            if (isExist)
            {
                return Result<User>.Failure(
                    "Email already exists!",
                    StatusCodes.Status400BadRequest
                );
            }

            var salt = appEncryption.GenerateSalt();
            var hashedPassword = appEncryption.HashPassword(password, salt);

            var user = new User()
            {
                Email = email,
                PhoneNo = phoneNo,
                Salt = salt,
                Password = hashedPassword,
                UserRole = role,
                UserStatus = UserStatus.Active
            };

            await authRepository.AddAsync(user);

            int res = await unitOfWork.SaveChangesAsync();

            if (res <= 0)
            {
                return Result<User>.Failure(
                    "Something went wrong, please try again later!",
                    StatusCodes.Status500InternalServerError
                );
            }

            return Result<User>.Success(user);
        }


        public async Task<Result<string>> SignUpAsync(SignupRequest model)
        {
            var result = await CreateUserAsync(
                model.Email,
                model.PhoneNo,
                model.Password,
                UserRole.Customer
            );

            if (!result.IsSuccess)
            {
                return Result<string>.Failure(
                    result.Message,
                    result.StatusCode
                );
            }

            return Result<string>.Success("Sign Up Successful!");
        }

        public async Task<Result<string>> CreateAdminAsync(SignupRequest model)
        {
            var result = await CreateUserAsync(
                model.Email,
                model.PhoneNo,
                model.Password,
                UserRole.Admin
            );

            if (!result.IsSuccess)
            {
                return Result<string>.Failure(
                    result.Message,
                    result.StatusCode
                );
            }

            return Result<string>.Success("Admin created successfully!");
        }

        public async Task<Result<string>> LoginAsync(LoginRequest model)
        {
            var user = await authRepository.FirstOrDefaultAsync(user => user.Email == model.Email);
            if (user is null)
            {
                return Result<string>.Failure("Invalid Credentials", StatusCodes.Status400BadRequest);
            }

            if (user.UserStatus != UserStatus.Active)
            {
                return Result<string>.Failure("User is not active", StatusCodes.Status403Forbidden);
            }

            var hashedPassword = appEncryption.HashPassword(model.Password, user.Salt);
            if (hashedPassword != user.Password)
            {
                return Result<string>.Failure("Invalid Credentials", StatusCodes.Status400BadRequest);
            }
            var token = jwtProvider.GenerateToken(user);
            return Result<string>.Success(token);
        }


        public async Task<Result<string>> ChangePassword(ChangePassword model)
        {
            var userId = contextService.GetId();
            var user = await authRepository.GetByIdAsync(userId);
            if (user is null)
            {
                return Result<string>.Failure("User not found", StatusCodes.Status404NotFound);
            }

            var oldPassword = appEncryption.HashPassword(model.OldPassword, user.Salt);

            if (oldPassword != user.Password)
            {
                return Result<string>.Failure("Wrong Password", StatusCodes.Status409Conflict);
            }

            var salt = appEncryption.GenerateSalt();
            var newPassword = appEncryption.HashPassword(model.NewPassword, salt);
            user.Salt = salt;
            user.Password = newPassword;

            await authRepository.UpdateAsync(user);
            var isUpdated = await unitOfWork.SaveChangesAsync();
            if (isUpdated > 0)
            {
                return Result<string>.Success("password changed successfully");
            }
            return Result<string>.Failure("Something went wrong", StatusCodes.Status500InternalServerError);

        }
    }
}

