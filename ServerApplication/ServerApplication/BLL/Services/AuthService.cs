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
        }

        public async Task<Guid> CreateNewUser(Registration registration)
        {
            if (registration.Password == registration.RepeatedPassword)
            {
                var user = new User { };
                var result = await userManager.CreateAsync(user, registration.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return new Guid(user.Id.ToByteArray()); // TODO return new Guid(user.Id);
                }
                throw new Exception("Couldn't create a User with the provided credentials!");
            }
            throw new Exception("Password and RepeatedPassword doesn't match!");
        }

        public AuthData GetAuthdata(Guid id)
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

            return new AuthData
            {
                Token = token,
                TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds(),
                Id = id.ToString()
            };
        }

        public async Task<Guid> LogInUser(Login login)
        {
            var result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if (result.Succeeded)
            {
                var user = userManager.Users.SingleOrDefault(u => u.UserName.ToLower() == login.UserName.ToLower());
                return new Guid(user.Id.ToByteArray());  // TODO return new Guid(user.Id);
            }
            throw new Exception("Wrong credentials!");
        }
    }
}
