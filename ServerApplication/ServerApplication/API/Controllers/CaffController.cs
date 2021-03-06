﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using ServerApplication.API.DTOs.CaffFile;
using ServerApplication.BLL.Models.CaffFile;
using ServerApplication.BLL.Models.CaffFile.DB;
using ServerApplication.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ServerApplication.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class CaffController : ControllerBase
    {
        IAuthService authService;
        ICaffService caffService;
        IMapper mapper;

        public CaffController(IAuthService authService, ICaffService caffService, IMapper mapper)
        {
            this.authService = authService;
            this.caffService = caffService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CaffFileData>>> GetOwnCaffFiles()
        {
            var userId = this.User.Claims.FirstOrDefault().Value;

            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var caffFiles = caffService.GetOwnCaffFiles(userId);
                return caffFiles.Select(mapper.Map<CaffFileData>).ToList();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CaffFileIdData>> PostCaffFile([FromBody] CaffFileData model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
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

        [HttpPost("{caffId}/upload"), DisableRequestSizeLimit]
        public async Task<ActionResult<CaffFileIdData>> UploadCaffFile(string caffId)
        {
            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);

                var file = Request.Form.Files[0];
                var id = caffService.UploadCaffFile(caffId, file);

                return new CaffFileIdData()
                {
                    Id = caffId
                };
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("{caffId}/download")]
        public async Task<IActionResult> Download(string caffId)
        {
            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var downloadableCaffFile = await caffService.DownloadCaffFile(caffId);
                return File(downloadableCaffFile.Memory, GetContentType(downloadableCaffFile.FilePath));
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
                await authService.CheckIfUserIsLoggedIn(userId);
                var caffFile = await caffService.ReturnCaffFile(caffId, userId);
                return mapper.Map<CaffFileData>(caffFile);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<CaffFileData>>> GetAllCaffFiles(string caffId)
        {
            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var caffFiles = caffService.GetAllCaffFiles();
                return caffFiles.Select(mapper.Map<CaffFileData>).ToList();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("{caffId}/preview")]
        public async Task<IActionResult> GetPreviewFile(string caffId)
        {
            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var downloadablePreview = await caffService.DownloadPreview(caffId);
                return File(downloadablePreview.Memory, GetContentType(downloadablePreview.FilePath));
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
                await authService.CheckIfUserIsLoggedIn(userId);
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
                await authService.CheckIfUserIsLoggedIn(userId);
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

        [HttpPost("{caffId}/comments")]
        public async Task<ActionResult<CommentIdData>> PostComment([FromBody] CommentData model, string caffId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var commentId = caffService.CreateNewComment(mapper.Map<Comment>(model), caffId, userId);
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

        [HttpGet("{caffId}/comments/{commentId}")]
        public async Task<ActionResult<CommentData>> GetComment(string caffId, string commentId)
        {
            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var comment = caffService.GetComment(commentId, caffId);
                return mapper.Map<CommentData>(comment);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("{caffId}/comments")]
        public async Task<ActionResult<List<CommentData>>> GetAllCommentsOfCaffFile(string caffId)
        {
            var userId = this.User.Claims.FirstOrDefault().Value;
            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
                var comments = caffService.GetCommentsOfCaffFile(caffId);
                return comments.Select(c => mapper.Map<CommentData>(c)).ToList();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPut("{caffId}/comments")]
        public async Task<ActionResult<CommentIdData>> UpdateComment([FromBody] CommentData model, string caffId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = this.User.Claims.FirstOrDefault().Value;

            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
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

        [HttpDelete("{caffId}/comments/{commentId}")]
        public async Task<ActionResult<CommentIdData>> DeleteComment(string caffId, string commentId)
        {
            var userId = this.User.Claims.FirstOrDefault().Value;

            try
            {
                await authService.CheckIfUserIsLoggedIn(userId);
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

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
