using Microsoft.AspNetCore.Identity;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.DTOs.User;
using SocialbookAPI.Application.Exceptions;
using SocialbookAPI.Domain.Entities.Identity;
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

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
                //var user = await _userManager.FindByEmailAsync(model.Email);
                //await _userManager.AddToRoleAsync(user, "Member");
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
            //var roles = await _userManager.GetRolesAsync(user);
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
                    //Role = roles.FirstOrDefault()


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
    }
}
