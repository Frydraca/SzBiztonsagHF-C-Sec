using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApplication.API.DTOs.CaffFile;
using ServerApplication.BLL.Models.CaffFile;
using ServerApplication.BLL.Models.CaffFile.DB;
using ServerApplication.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet]
        public ActionResult<List<CaffFileData>> GetOwnCaffFiles()
        {
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

        [HttpPost]
        public ActionResult<CaffFileIdData> PostCaffFile([FromBody] CaffFileData model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                var caffFileId = caffService.CreateNewCaffFile(mapper.Map<CaffFile>(model), userId);
                return new CaffFileIdData()
                {
                    Id = caffFileId
                };
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("{caffId}")]
        public async Task<ActionResult<CaffFileData>> GetCaffFile(string caffId)
        {
            var userId = this.User.Claims.FirstOrDefault().Value;

            try
            {
                var caffFile = await caffService.ReturnCaffFile(caffId, userId);
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

        [HttpPut]
        public async Task<ActionResult<CaffFileIdData>> UpdateCaffFile([FromBody] CaffFileData model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                var caffFileId = await caffService.UpdateCaffFile(mapper.Map<CaffFile>(model), userId);
                return new CaffFileIdData()
                {
                    Id = caffFileId
                };
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpDelete("{caffid}")]
        public async Task<ActionResult<CaffFileIdData>> DeleteCaffFile(string caffId)
        {
            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                var caffFileId = await caffService.DeleteCaffFile(caffId, userId);
                return new CaffFileIdData()
                {
                    Id = caffFileId
                };
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost("{caffId}/create")]
        public ActionResult<CommentIdData> PostComment([FromBody] CommentData model, string caffId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var commentId = caffService.CreateNewComment(mapper.Map<Comment>(model), caffId);
                return new CommentIdData()
                {
                    Id = commentId
                };
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("{caffId}/{commentId}")]
        public ActionResult<CommentData> GetComment(string caffId, string commentId)
        {
            try
            {
                var comment = caffService.GetComment(commentId, caffId);
                return mapper.Map<CommentData>(comment);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("{caffId}/comments")]
        public ActionResult<List<CommentData>> GetCommentsOfCaffFile(string caffId)
        {
            try
            {
                var comments = caffService.GetCommentsOfCaffFile(caffId);
                return comments.Select(c => mapper.Map<CommentData>(c)).ToList();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPut("{caffId}")]
        public async Task<ActionResult<CommentIdData>> UpdateComment([FromBody] CommentData model, string caffId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = this.User.Claims.FirstOrDefault().Value;

            try
            {
                var updatedCommentId = await caffService.UpdateComment(mapper.Map<Comment>(model), caffId, userId);
                return new CommentIdData()
                {
                    Id = updatedCommentId
                }; ;
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpDelete("{caffId}/{commentId}")]
        public async Task<ActionResult<CommentIdData>> DeleteComment(string caffId, string commentId)
        {
            var userId = this.User.Claims.FirstOrDefault().Value;

            try
            {
                var deletedCommentId = await caffService.DeleteComment(commentId, caffId, userId);
                return new CommentIdData()
                {
                    Id = deletedCommentId
                }; ;
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

    }
}
