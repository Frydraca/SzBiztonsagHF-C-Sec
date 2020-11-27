using ServerApplication.BLL.Models.CaffFile.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Services.Interfaces
{
    public interface ICaffService
    {
        public string CreateNewCaffFile(CaffFile newCaffFile);
        public IEnumerable<CaffFile> GetAllCaffFiles();
        public IEnumerable<CaffFile> GetOwnCaffFiles(string userId);
        public CaffFile GetCaffFile(string caffFileId, string userId);
        public string UpdateCaffFile(CaffFile updatedCaffFile, string userId);
        public string DeleteCaffFile(string caffFileId, string userId);
        public string CreateNewComment(Comment newComment, string parentCaffId);
        public Comment GetComment(string commentId, string userId);
        public IEnumerable<Comment> GetCommentsOfCaffFile(string parentCaffId);
        public string UpdateComment(Comment updatedComment, string userId);
        public string DeleteComment(string commentId, string userId);


    }
}
