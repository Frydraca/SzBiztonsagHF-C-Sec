using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServerApplication.API.DTOs.CaffFile;
using ServerApplication.BLL.Models.CaffFile.DB;
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

        [HttpPost("create-caff-file")]
        public ActionResult<CaffFileIdData> PostCaffFile([FromBody] CaffFileData model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var responseId = new CaffFileIdData()
                {
                    Id = caffService.CreateNewCaffFile(mapper.Map<CaffFile>(model))
                };
                return responseId;
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CaffFileData> GetCaffFile(string caffId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = this.User.Claims.FirstOrDefault().Value;

            try
            {
                var caffFile = caffService.GetCaffFile(caffId, userId);
                return mapper.Map<CaffFileData>(caffFile);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("all")]
        public ActionResult<List<CaffFileData>> GetAllCaffFiles()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var caffFiles = caffService.GetAllCaffFiles();
                return caffFiles.Select(mapper.Map<CaffFileData>).ToList();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("own-caff-files")]
        public ActionResult<List<CaffFileData>> GetOwnCaffFiles()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = this.User.Claims.FirstOrDefault().Value;

            try
            {
                var caffFiles = caffService.GetOwnCaffFiles(userId);
                return caffFiles.Select(mapper.Map<CaffFileData>).ToList();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

    }
}
