using ServerApplication.BLL.Models.User;
using ServerApplication.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Services
{
    public class UserDataService : IUserDataService
    {
        public bool ChangeUserName(string askingUserId, UserNameChange userNameChange)
        {
            throw new NotImplementedException();
        }
    }
}
