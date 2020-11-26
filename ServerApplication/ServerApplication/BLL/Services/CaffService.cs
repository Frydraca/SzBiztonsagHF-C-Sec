using MongoDB.Bson;
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

        public string CreateNewCaffFile(CaffFile newCaffFile)
        {
            var id = ObjectId.GenerateNewId().ToString();
            newCaffFile.Id = id;
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

        public CaffFile GetCaffFile(string caffFileId, string userId)
        {
            if (!hasAccessToCaffFile(caffFileId, userId))
            {
                throw new Exception("You have no access to this caff file!");
            }

            return caffFileRepository.Find(caffFileId);
        }

        public IEnumerable<CaffFile> GetOwnCaffFiles(string userId)
        {
            return caffFileRepository.Query(caffFile => caffFile.Owner == userId);
        }

        private bool hasAccessToCaffFile(string caffFileId, string userId)
        {
            var caffFile = caffFileRepository.Find(caffFileId);

            return caffFile.Owner == userId;
        }
    }
}
