using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.API.DTOs.Auth
{
    public class RegistrationData
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string RepeatedPassword { get; set; }
    }
}
