using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.API.DTOs.Auth
{
    public class AuthData
    {
        public string Token { get; set; }

        public long TokenExpirationTime { get; set; }

        public string Id { get; set; }

        public bool IsAdmin { get; set; }
    }
}
