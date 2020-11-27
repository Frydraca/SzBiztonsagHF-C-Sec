using ServerApplication.BLL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Services.Interfaces
{
    public interface IUserDataService
    {
        public bool ChangeUserName(string askingUserId, UserNameChange userNameChange);
    }
}
