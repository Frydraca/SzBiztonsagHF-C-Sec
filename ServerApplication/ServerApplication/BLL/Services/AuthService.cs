using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ServerApplication.API.DTOs.Auth;
using ServerApplication.BLL.Models.Auth;
using ServerApplication.BLL.Models.User.DB;
using ServerApplication.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Services
{
    public class AuthService : IAuthService
    {
        string jwtSecret;
        int jwtLifespan;
        UserManager<User> userManager;
        SignInManager<User> signInManager;

        public AuthService(string jwtSecret, int jwtLifespan, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.jwtSecret = jwtSecret;
            this.jwtLifespan = jwtLifespan;
            this.userManager = userManager;
            this.signInManager = signInManager;

            checkForAdmin();

        }

        public async Task CheckIfUserIsLoggedIn(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if(user.IsLoggedIn) return;
            throw new Exception("User is not logged in!");
        }

        public async Task<Guid> CreateNewUser(Registration registration)
        {
            if (registration.Password == registration.RepeatedPassword)
            {
                var user = new User { UserName = registration.UserName, IsAdmin = false, IsLoggedIn = true};
                var result = await userManager.CreateAsync(user, registration.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return new Guid(user.Id);
                }
                throw new Exception("Couldn't create a User with the provided credentials!");
            }
            throw new Exception("Password and RepeatedPassword doesn't match!");
        }

        public async Task<AuthData> GetAuthdata(Guid id)
        {
            var expirationTime = DateTime.UtcNow.AddDays(jwtLifespan);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            var user = await userManager.FindByIdAsync(id.ToString());

            return new AuthData
            {
                Token = token,
                TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds(),
                Id = id.ToString(),
                IsAdmin = user.IsAdmin
            };
        }

        public async Task<Guid> LogInUser(Login login)
        {
            var result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if (result.Succeeded)
            {
                var user = userManager.Users.SingleOrDefault(u => u.UserName.ToLower() == login.UserName.ToLower());
                user.IsLoggedIn = true;
                var res = await userManager.UpdateAsync(user);
                if(res.Succeeded)
                {
                    return new Guid(user.Id);
                }
                throw new Exception("Couldn't log in user!");
            }
            throw new Exception("Wrong credentials!");
        }

        private async void checkForAdmin()
        {
            var result = await userManager.FindByNameAsync("admin");

            if (result == null)
            {
                var admin = new User { UserName = "admin" , IsAdmin = true };
                await userManager.CreateAsync(admin, "SecretPassword");
            }
        }
    }
}
