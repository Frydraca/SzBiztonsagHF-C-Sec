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
        public CaffFile GetCaffFile(string caffFileId);
    }
}
