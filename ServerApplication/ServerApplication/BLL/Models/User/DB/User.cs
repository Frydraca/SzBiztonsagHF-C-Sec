using AspNetCore.Identity.Mongo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Models.User.DB
{
    public class User : MongoUser
    {
        public bool IsAdmin { get; set; }
    }
}
