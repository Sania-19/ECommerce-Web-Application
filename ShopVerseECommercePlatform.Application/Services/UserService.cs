using Microsoft.AspNetCore.Http;
using ShopVerseECommercePlatform.Application.RRModels.Users;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using ShopVerseECommercePlatform.Application.Abstraction.IUnitOfWork;
using ShopVerseECommercePlatform.Application.Utils.Result;
using System;
using System.Collections.Generic;
using System.Text;
using static ShopVerseECommercePlatform.Domain.Enum;


namespace ShopVerseECommercePlatform.Application.Services
{
    public class UserService(IUserRepository userRepository, IUnitOfWork unitOfWork) : IUserService
    {
        #region READ
        public async Task<Result<IEnumerable<UserResponse>>> GetUsers()
        {
            var res = (await userRepository.GetAllAsync()).Select(x => new UserResponse
            {
                Id = x.Id,
                Email = x.Email,
                PhoneNo = x.PhoneNo,
                UserRole = x.UserRole,
                UserStatus = x.UserStatus

            });
            if (res is null || !res.Any() || res.Count() == 0)
            {
                return Result<IEnumerable<UserResponse>>.Failure("Users Not found", StatusCodes.Status404NotFound);
            }
            return Result<IEnumerable<UserResponse>>.Success(res);
        }
        public async Task<Result<IEnumerable<UserResponse>>> GetUserByEmail(string email)
        {
            var users = await userRepository.FindByAsync(x => x.Email.StartsWith(email));
            var userList = users.Select(x => new UserResponse
            {
                Id = x.Id,
                Email = x.Email,
                PhoneNo = x.PhoneNo,
                UserRole = x.UserRole,
                UserStatus = x.UserStatus
            }).ToList();
            if (userList is null || userList.Count == 0)
            {
                return Result<IEnumerable<UserResponse>>.Failure("No match found", StatusCodes.Status404NotFound);
            }
            return Result<IEnumerable<UserResponse>>.Success(userList);
        }

        public async Task<Result<UserResponse>> GetUserById(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);
            UserResponse userResponse = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNo = user.PhoneNo,
                UserRole = user.UserRole,
                UserStatus = user.UserStatus
            };
            if (user is null)
            {
                return Result<UserResponse>.Failure("No match found", StatusCodes.Status404NotFound);
            }
            return Result<UserResponse>.Success(userResponse);
        }

        public async Task<Result<IEnumerable<UserResponse>>> GetUsersByRoles(IEnumerable<string> userRoles)
        {
            var roles = userRoles
                .Select(r => Enum.Parse<UserRole>(r, true))
                .ToList();

            var users = await userRepository.FindByAsync(x => roles.Contains(x.UserRole));

            var userList = users
                .OrderByDescending(x => x.UserRole == UserRole.SuperAdmin) // superadmins first
                .ThenBy(x => x.Email)
                .Select(x => new UserResponse
                {
                    Id = x.Id,
                    Email = x.Email,
                    PhoneNo = x.PhoneNo,
                    UserRole = x.UserRole,
                    UserStatus = x.UserStatus
                })
                .ToList();

            if (userList.Count == 0)
            {
                return Result<IEnumerable<UserResponse>>.Failure("No match found", StatusCodes.Status404NotFound);
            }

            return Result<IEnumerable<UserResponse>>.Success(userList);
        }
        #endregion

        
        public async Task<Result<string>> UpdateUserStatus(UserStatus userStatus, Guid userId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                return Result<string>.Failure("No user found", StatusCodes.Status404NotFound);
            }

            user.UserStatus = userStatus;
            await userRepository.UpdateAsync(user);
            int returnValue = await unitOfWork.SaveChangesAsync();


            if (returnValue > 0)
            {
                return Result<string>.Success(message: "User Status updated Successfully");
            }

            return Result<string>.Failure("Something went wrong", StatusCodes.Status500InternalServerError);
        }
    }
}
