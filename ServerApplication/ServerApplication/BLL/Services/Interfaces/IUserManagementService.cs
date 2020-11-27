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
        public Task<bool> UpdateUser(User targetUser, User askingUser);

        public Task<bool> ChangeUserPassword(User targetUser, User askingUser, ChangePassword changePassword);

        public Task<User> GetUser(string id);
    }
}
