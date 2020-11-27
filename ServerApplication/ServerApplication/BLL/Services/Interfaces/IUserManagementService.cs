using ServerApplication.BLL.Models.User;
using ServerApplication.BLL.Models.User.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Services.Interfaces
{
    public interface IUserManagementService
    {
        public Task<string> UpdateUser(User targetUser, string askingUserId);

        public Task<string> ChangeUserPassword(User targetUser, string askingUserId, ChangePassword changePassword);

        public Task<User> GetUser(string id);

        public Task<List<User>> GetAllUsers(string askingUserId);
    }
}
