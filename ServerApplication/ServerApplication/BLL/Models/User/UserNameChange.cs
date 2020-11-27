using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Models.User
{
    public class UserNameChange
    {
        public string NewUserName { get; set; }

        public string TargetUserId { get; set; }
    }
}
