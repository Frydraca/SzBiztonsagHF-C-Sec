using MongoDB.Bson;
using ServerApplication.BLL.Models.CaffFile;
using ServerApplication.BLL.Models.CaffFile.DB;
using ServerApplication.BLL.RepositoryInterfaces;
using ServerApplication.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Services
{
    public class CaffService : ICaffService
    {
        private ICaffFileRepository caffFileRepository;

        public CaffService(ICaffFileRepository caffFileRepository)
        {
            this.caffFileRepository = caffFileRepository;
        }

        public string CreateNewCaffFile(CaffFile newCaffFile, string askingUserId)
        {
            var id = ObjectId.GenerateNewId().ToString();
            newCaffFile.Id = id;
            newCaffFile.Owner = askingUserId;
            newCaffFile.CreationDate = DateTime.Now;

            newCaffFile.Comments.ForEach(c =>
            {
                c.Id = ObjectId.GenerateNewId().ToString();
                c.CreationDate = newCaffFile.CreationDate;
                c.Owner = askingUserId;
            });

            if (caffFileRepository.Create(newCaffFile))
            {
                return id;
            }
            else
            {
                throw new Exception("Couldn't create a new caff file!");
            }
        }

        public IEnumerable<CaffFile> GetAllCaffFiles()
        {
            return caffFileRepository.QueryAll();
        }

        public CaffFile GetCaffFile(string caffFileId, string askingUserId)
        {
            if (!hasAccessToCaffFile(caffFileId, askingUserId))
            {
                throw new Exception("You have no access to this caff file!");
            }

            return caffFileRepository.Find(caffFileId);
        }

        public IEnumerable<CaffFile> GetOwnCaffFiles(string askingUserId)
        {
            return caffFileRepository.Query(caffFile => caffFile.Owner == askingUserId);
        }

        public string UpdateCaffFile(CaffFile updatedCaffFile, string userId)
        {
            if (!hasAccessToCaffFile(updatedCaffFile.Id, userId))
            {
                throw new Exception("You have no access to this caff file!");
            }
            if (caffFileRepository.Update(updatedCaffFile))
            {
                return updatedCaffFile.Id;
            }

            throw new Exception("Couldn't update this caff file!");
        }

        public string DeleteCaffFile(string caffFileId, string userId)
        {
            if (!hasAccessToCaffFile(caffFileId, userId))
            {
                throw new Exception("You have no access to this caff file!");
            }
            if (caffFileRepository.Delete(caffFileId))
            {
                return caffFileId;
            }

            throw new Exception("Couldn't delete this caff file!");
        }

        public string CreateNewComment(Comment newComment, string parentCaffId)
        {
            throw new NotImplementedException();
        }

        public Comment GetComment(string commentId, string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetCommentsOfCaffFile(string parentCaffId)
        {
            throw new NotImplementedException();
        }

        public string UpdateComment(Comment updatedComment, string userId)
        {
            throw new NotImplementedException();
        }
        public string DeleteComment(string commentId, string userId)
        {
            throw new NotImplementedException();
        }

        private bool hasAccessToCaffFile(string caffFileId, string userId)
        {
            var caffFile = caffFileRepository.Find(caffFileId);

            return caffFile.Owner == userId;
        }
    }
}
