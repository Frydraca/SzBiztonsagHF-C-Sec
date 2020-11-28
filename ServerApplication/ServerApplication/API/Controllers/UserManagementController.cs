using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApplication.API.DTOs.User;
using ServerApplication.BLL.Models.User;
using ServerApplication.BLL.Models.User.DB;
using ServerApplication.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        IAuthService authService;
        IUserManagementService userManagementService;
        IMapper mapper;

        public UserManagementController(IMapper mapper, IAuthService authService, IUserManagementService userManagementService)
        {
            this.authService = authService;
            this.userManagementService = userManagementService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<UserData>> GetAsync()
        {
            var userId = this.User.Claims.FirstOrDefault().Value;

            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var user = await userManagementService.GetUser(userId);

                return mapper.Map<UserData>(user);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UserData>>> GetAllAsync()
        {
            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var users = await userManagementService.GetAllUsers(userId);

                return users.Select(mapper.Map<UserData>).ToList();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<UserUpdateResponseData>> Edit([FromBody] UserData model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = this.User.Claims.FirstOrDefault().Value;
            var targetUser = mapper.Map<User>(model);
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var result = await userManagementService.UpdateUser(targetUser, userId);

                return new UserUpdateResponseData() { UpdatedUserId = result };
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost("change-password")]
        public async Task<ActionResult<UserUpdateResponseData>> ChangePassword([FromBody] ChangePasswordData model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = this.User.Claims.FirstOrDefault().Value;
            var targetUser = mapper.Map<User>(model);
            var changePassword = mapper.Map<ChangePassword>(model);
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var result = await userManagementService.ChangeUserPassword(targetUser, userId, changePassword);

                return new UserUpdateResponseData() { UpdatedUserId = result };
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<UserUpdateResponseData>> DeleteUser([FromBody] UserData model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = this.User.Claims.FirstOrDefault().Value;
            var targetUser = mapper.Map<User>(model);
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var result = await userManagementService.DeleteUser(targetUser, userId);

                return new UserUpdateResponseData() { UpdatedUserId = result };
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
