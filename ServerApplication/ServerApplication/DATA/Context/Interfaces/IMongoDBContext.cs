using MongoDB.Driver;
using ServerApplication.BLL.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.DATA.Context.Interfaces
{
    public interface IMongoDBContext
    {
        public IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : DBModel;
    }
}
