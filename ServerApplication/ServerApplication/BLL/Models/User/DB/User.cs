using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Models.User.DB
{
    public class User
    {
        // TODO inherit from MongoDB or something
        // TEMPORARY parameters - Username, Id
        public string UserName { get; set; }

        public Guid Id { get; set; }
    }
}
