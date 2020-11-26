using ServerApplication.BLL.Models.CaffFile.DB;
using ServerApplication.BLL.RepositoryInterfaces;
using ServerApplication.DATA.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.DATA.Repositories
{
    public class CaffFileRepository : BaseRepository<CaffFile>, ICaffFileRepository
    {
        public CaffFileRepository(IMongoDBContext dbContext) : base(dbContext)
        {

        }
    }
}
