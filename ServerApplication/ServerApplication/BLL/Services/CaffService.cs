using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using ServerApplication.BLL.Models.CaffFile;
using ServerApplication.BLL.Models.CaffFile.DB;
using ServerApplication.BLL.Models.User.DB;
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
        UserManager<User> userManager;

        public CaffService(ICaffFileRepository caffFileRepository, UserManager<User> userManager)
        {
            this.caffFileRepository = caffFileRepository;
            this.userManager = userManager;
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

        public async Task<CaffFile> ReturnCaffFile(string caffFileId, string askingUserId)
        {
            var askingUser = await userManager.FindByIdAsync(askingUserId);
            if (!hasAccessToCaffFile(caffFileId, askingUser))
            {
                throw new Exception("You have no access to this caff file!");
            }

            return caffFileRepository.Find(caffFileId);
        }

        public IEnumerable<CaffFile> GetOwnCaffFiles(string askingUserId)
        {
            return caffFileRepository.Query(caffFile => caffFile.Owner == askingUserId);
        }

        public async Task<string> UpdateCaffFile(CaffFile updatedCaffFile,string caffFileId, string askingUserId)
        {
            var askingUser = await userManager.FindByIdAsync(askingUserId);

            if (!hasAccessToCaffFile(caffFileId, askingUser))
            {
                throw new Exception("You have no access to this caff file!");
            }

            CaffFile oldCaffFile = FindExistingCaffFile(caffFileId);
            updatedCaffFile.Id = oldCaffFile.Id;
            updatedCaffFile.FilePath = oldCaffFile.FilePath;
            updatedCaffFile.Owner = oldCaffFile.Owner;
            if (caffFileRepository.Update(updatedCaffFile))
            {
                return updatedCaffFile.Id;
            }

            throw new Exception("Couldn't update this caff file!");
        }

        public async Task<string> DeleteCaffFile(string caffFileId, string askingUserId)
        {
            var askingUser = await userManager.FindByIdAsync(askingUserId);
            if (!hasAccessToCaffFile(caffFileId, askingUser))
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
            CaffFile parentCaffFile = FindExistingCaffFile(parentCaffId);

            parentCaffFile.Comments.Add(newComment);
            if (caffFileRepository.Update(parentCaffFile))
            {
                return newComment.Id;
            }
            throw new Exception("Couldn't add the comment to the caff file!");

        }

        public Comment GetComment(string commentId, string parentCaffId)
        {
            CaffFile parentCaffFile = FindExistingCaffFile(parentCaffId);

            Comment requestedComment = parentCaffFile.Comments.Find(c => c.Id == commentId);
            if(requestedComment == null)
            {
                throw new Exception("Couldn't find the requested comment!");
            }
            return requestedComment;
        }

        public IEnumerable<Comment> GetCommentsOfCaffFile(string parentCaffId)
        {
            CaffFile parentCaffFile = FindExistingCaffFile(parentCaffId);

            return parentCaffFile.Comments;
        }

        public async Task<string> UpdateComment(Comment updatedComment, string parentCaffId, string askingUserId)
        {
            var askingUser = await userManager.FindByIdAsync(askingUserId);
            if (!hasAccessToCaffFile(parentCaffId, askingUser))
            {
                throw new Exception("You have no access to this caff file!");
            }
            CaffFile parentCaffFile = FindExistingCaffFile(parentCaffId);

            Comment oldComment = parentCaffFile.Comments.Find(c => c.Id == updatedComment.Id);
            if (oldComment == null)
            {
                throw new Exception("Couldn't find the comment to update!");
            }
            parentCaffFile.Comments.Remove(oldComment);
            parentCaffFile.Comments.Add(updatedComment);
            return updatedComment.Id;
        }
        public async Task<string> DeleteComment(string commentId, string parentCaffId, string askingUserId)
        {
            var askingUser = await userManager.FindByIdAsync(askingUserId);
            if (!hasAccessToCaffFile(parentCaffId, askingUser))
            {
                throw new Exception("You have no access to this caff file!");
            }
            CaffFile parentCaffFile = FindExistingCaffFile(parentCaffId);

            Comment oldComment = parentCaffFile.Comments.Find(c => c.Id == commentId);
            if (oldComment == null)
            {
                throw new Exception("Couldn't find the comment to delete!");
            }
            parentCaffFile.Comments.Remove(oldComment);
            return commentId;
        }

        private CaffFile FindExistingCaffFile(string caffFileId)
        {
            CaffFile caffFile = caffFileRepository.Find(caffFileId);
            if (caffFile == null)
            {
                throw new Exception("Couldn't find the requested caff file!");
            }
            return caffFile;
        }

        private bool hasAccessToCaffFile(string caffFileId, User askingUser)
        {
            var caffFile = caffFileRepository.Find(caffFileId);

            return caffFile.Owner == askingUser.Id || askingUser.IsAdmin;
        }
    }
}
