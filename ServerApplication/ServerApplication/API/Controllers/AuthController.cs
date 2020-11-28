using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServerApplication.API.DTOs.Auth;
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
