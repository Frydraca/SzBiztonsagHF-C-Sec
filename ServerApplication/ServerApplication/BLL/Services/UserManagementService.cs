using Microsoft.AspNetCore.Identity;
using ServerApplication.BLL.Models.User;
using ServerApplication.BLL.Models.User.DB;
using ServerApplication.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Services
{
    public class UserManagementService : IUserManagementService
    {
        UserManager<User> userManager;

        public UserManagementService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> ChangeUserPassword(User targetUser, string askingUserId, ChangePassword changePassword)
        {
            var askingUser = await userManager.FindByIdAsync(askingUserId);
            if (!hasAccessToUserData(targetUser, askingUser))
            {
                throw new Exception("You have no access to this user!");
            }
            
            if (askingUser.IsAdmin)
            {
                var resetToken = await userManager.GeneratePasswordResetTokenAsync(targetUser);
                var result = await userManager.ResetPasswordAsync(targetUser, resetToken, changePassword.NewPassword);
                if (result.Succeeded)
                {
                    return targetUser.Id;                  
                }
                throw new Exception("Password change failed!");
            }
            else
            {
                if (changePassword.NewPassword != changePassword.RepeatedNewPassword)
                {
                    throw new Exception("The passwords are different!");
                }

                var result = await userManager.ChangePasswordAsync(targetUser, changePassword.OldPassword, changePassword.NewPassword);
                if(result.Succeeded)
                {
                    return targetUser.Id;
                }
                throw new Exception("Wrong password!");
            }
            
        }
        public async Task<string> UpdateUser(User targetUser, string askingUserId)
        {
            var askingUser = await userManager.FindByIdAsync(askingUserId);
            if (!hasAccessToUserData(targetUser, askingUser))
            {
                throw new Exception("You have no access to this user!"); 
            }
            
            var result = await userManager.UpdateAsync(targetUser);
            if(result.Succeeded)
            {
                return targetUser.Id;
            }
            throw new Exception("Update was unsuccessfull!");
            
        }

        public async Task<User> GetUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                return user;
            }
            throw new Exception("Couldn't find the user!");
        }

        public async Task<List<User>> GetAllUsers(string askingUserId)
        {
            var askingUser = await userManager.FindByIdAsync(askingUserId);
            if (!askingUser.IsAdmin)
            {
                throw new Exception("You have no access to all the users!");
            }
            return userManager.Users.ToList();
        }

        public async Task<string> DeleteUser(User targetUser, string askingUserId)
        {
            var askingUser = await userManager.FindByIdAsync(askingUserId);
            if (!hasAccessToUserData(targetUser, askingUser))
            {
                throw new Exception("You have no access to this user!");
            }

            var result = await userManager.DeleteAsync(targetUser);
            if (result.Succeeded)
            {
                return targetUser.Id;
            }
            throw new Exception("Update was unsuccessfull!");
        }

        private bool hasAccessToUserData(User targetUser, User askingUser)
        {
            return askingUser.IsAdmin || targetUser.Id == askingUser.Id;
        }
    }
}
