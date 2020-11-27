using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServerApplication.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaffController : ControllerBase
    {
        ICaffService caffService;
        IMapper mapper;

        public CaffController(ICaffService caffService, IMapper mapper)
        {
            this.caffService = caffService;
            this.mapper = mapper;
        }
    }
}
