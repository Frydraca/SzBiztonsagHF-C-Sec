using AutoMapper;
using ServerApplication.API.DTOs.Auth;
using ServerApplication.API.DTOs.UserDataChange;
using ServerApplication.BLL.Models.Auth;
using ServerApplication.BLL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationData, Registration>();
            CreateMap<LoginData, Login>();
            CreateMap<UserNameChangeData, UserNameChange>();
        }
    }
}
