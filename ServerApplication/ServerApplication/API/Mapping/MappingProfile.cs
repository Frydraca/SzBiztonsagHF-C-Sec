using AutoMapper;
using ServerApplication.API.DTOs.Auth;
using ServerApplication.API.DTOs.CaffFile;
using ServerApplication.API.DTOs.User;
using ServerApplication.BLL.Models.Auth;
using ServerApplication.BLL.Models.CaffFile.DB;
using ServerApplication.BLL.Models.User;
using ServerApplication.BLL.Models.User.DB;
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
            CreateMap<UserData, User>();
            CreateMap<User, UserData>();
            CreateMap<ChangePasswordData, User>();
            CreateMap<ChangePasswordData, ChangePassword>();
            CreateMap<CaffFileData, CaffFile>();
            CreateMap<CaffFile, CaffFileData>();
        }
    }
}
