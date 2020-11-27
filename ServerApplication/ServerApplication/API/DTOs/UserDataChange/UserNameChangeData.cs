using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.API.DTOs.UserDataChange
{
    public class UserNameChangeData
    {
        public string NewUserName { get; set; }

        public string TargetUserId { get; set; }
    }
}
