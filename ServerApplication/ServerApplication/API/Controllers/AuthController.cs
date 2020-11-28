using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApplication.API.DTOs.Auth;
using ServerApplication.API.DTOs.User;
using ServerApplication.BLL.Models.Auth;
using ServerApplication.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        IAuthService authService;
        IMapper mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthData>> PostAsync([FromBody]LoginData model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var id = await authService.LogInUser(mapper.Map<Login>(model));
                return await authService.GetAuthdata(id);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("logout")]
        public async Task<ActionResult<UserUpdateResponseData>> LogOut()
        {
            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var id = await authService.LogOutUser(userId);
                return new UserUpdateResponseData()
                {
                    UpdatedUserId = id
                };
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("refresh-jwt")]
        public async Task<ActionResult<AuthData>> RefreshJWT()
        {
            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                return await authService.GetAuthdata(new Guid(userId));
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthData>> PostAsync([FromBody]RegistrationData model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var id = await authService.CreateNewUser(mapper.Map<Registration>(model));

                return await authService.GetAuthdata(id);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
