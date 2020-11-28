using ServerApplication.API.DTOs.Auth;
using ServerApplication.BLL.Models.Auth;
using System;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<Guid> CreateNewUser(Registration registration);

        public Task<Guid> LogInUser(Login login);

        public Task<AuthData> GetAuthdata(Guid id);

        public Task CheckIfUserIsLoggedIn(string userId);
    }
}
