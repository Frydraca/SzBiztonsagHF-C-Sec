using ServerApplication.BLL.Models.CaffFile.DB;
using ServerApplication.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Services
{
    public class CaffService : ICaffService
    {
        public string CreateNewCaffFile(CaffFile newCaffFile)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CaffFile> GetAllCaffFiles()
        {
            throw new NotImplementedException();
        }

        public CaffFile GetCaffFile(string caffFileId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CaffFile> GetOwnCaffFiles(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
