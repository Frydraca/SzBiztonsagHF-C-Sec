using ServerApplication.BLL.Models.CaffFile.DB;
using ServerApplication.BLL.Models.CaffFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Services.Interfaces
{
    public interface ICaffService
    {
        public string CreateNewCaffFile(CaffFile newCaffFile, string askingUserId);
        public IEnumerable<CaffFile> GetAllCaffFiles();
        public IEnumerable<CaffFile> GetOwnCaffFiles(string askingUserId);
        public Task<CaffFile> ReturnCaffFile(string caffFileId, string askingUserId);
        public Task<string> UpdateCaffFile(CaffFile updatedCaffFile, string askingUserId);
        public Task<string> DeleteCaffFile(string caffFileId, string askingUserId);
        public string CreateNewComment(Comment newComment, string parentCaffId);
        public Comment GetComment(string commentId, string askingUserId);
        public IEnumerable<Comment> GetCommentsOfCaffFile(string parentCaffId);
        public Task<string> UpdateComment(Comment updatedComment, string parentCaffId, string askingUserId);
        public Task<string> DeleteComment(string commentId, string parentCaffId, string askingUserId);


    }
}
