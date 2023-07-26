using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.DTOs.User;
using SocialbookAPI.Application.Exceptions;
using SocialbookAPI.Application.Helpers;
using SocialbookAPI.Application.Repositories;
using SocialbookAPI.Domain.Entities;
using SocialbookAPI.Domain.Entities.Identity;
using SocialbookAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        private readonly SocialbookDbContext _context;
        readonly IEndpointReadRepository _endpointReadRepository;




        public UserService(UserManager<AppUser> userManager, SocialbookDbContext context, IEndpointReadRepository endpointReadRepository)
        {
            _userManager = userManager;
            _context = context;
            _endpointReadRepository = endpointReadRepository;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                NameSurname = model.NameSurname,
                Email = model.Email,
                Level = 1,
                Exp = 0,
                VoteCount = 1,
                Title = "Acemi Dj"
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
            {
                response.Message = "Kullanıcı başarıyla oluşturulmuştur!";
                var user = await _userManager.FindByEmailAsync(model.Email);
                await _userManager.AddToRoleAsync(user, "Member");
            }
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}<br>";

            return response;
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {


            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);

            }
            else
                throw new NotFoundUserException();

        }

        public async Task<UserResponse> GetUserAsync(string refreshToken)
        {
            AppUser? user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null)
            {
                return new()
                {
                    NameSurname = user.NameSurname,
                    Username = user.UserName,
                    Email = user.Email,
                    Level = user.Level,
                    Exp = user.Exp,
                    VoteCount = user.VoteCount,
                    Title = user.Title,
                    UserId = user.Id,
                    Role = roles.FirstOrDefault()


                };
            }
            else
                throw new NotFoundUserException();
        }

        public async Task UpdateUserInfos(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundUserException();

            }

            if (user.VoteCount > 0)
            {
                user.VoteCount--;
            }
            else
            {
                throw new Exception("User's VoteCount is already zero, can't decrease further");
            }

            user.Exp += 2;

            // If user's experience reaches or exceeds 100, increase user level and reset experience
            if (user.Exp >= 100)
            {
                user.Level++;
                user.Exp = 0;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Error updating user VoteCount");
            }
        }

        public async Task UpdateUserVoteCountsBasedOnLevels()
        {
            // Assuming _userManager.Users gives a list of all users
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                if (user.Level >= 1 && user.Level <= 20)
                {
                    user.VoteCount = 1;
                }
                else if (user.Level > 20 && user.Level <= 50)
                {
                    user.VoteCount = 2;
                }
                else if (user.Level > 50 && user.Level <= 100)
                {
                    user.VoteCount = 3;
                }
                // Continue with additional conditions as needed...
            }

            await _context.BulkUpdateAsync(users);
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();
            }
        }

        public async Task<List<ListUser>> GetAllUsersAsync(int page, int size)
        {
            var users = await _userManager.Users
                  .Skip(page * size)
                  .Take(size)
                  .ToListAsync();

            return users.Select(user => new ListUser
            {
                Id = user.Id,
                UserName = user.UserName,
                NameSurname = user.NameSurname,
                Email = user.Email,
                TwoFactorEnabled = user.TwoFactorEnabled

            }).ToList();
        }

        public int TotalUsersCount => _userManager.Users.Count();

        public async Task AssignRoleToUserAsnyc(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            return new string[] { };
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);

            if (!userRoles.Any())
                return false;

            Endpoint? endpoint = await _endpointReadRepository.Table
                     .Include(e => e.Roles)
                     .FirstOrDefaultAsync(e => e.Code == code);

            if (endpoint == null)
                return false;

            var hasRole = false;
            var endpointRoles = endpoint.Roles.Select(r => r.Name);

            //foreach (var userRole in userRoles)
            //{
            //    if (!hasRole)
            //    {
            //        foreach (var endpointRole in endpointRoles)
            //            if (userRole == endpointRole)
            //            {
            //                hasRole = true;
            //                break;
            //            }
            //    }
            //    else
            //        break;
            //}

            //return hasRole;

            foreach (var userRole in userRoles)
            {
                foreach (var endpointRole in endpointRoles)
                    if (userRole == endpointRole)
                        return true;
            }

            return false;
        }


    
    }
}
