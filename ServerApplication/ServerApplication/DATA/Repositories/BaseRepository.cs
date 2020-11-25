using MongoDB.Bson;
using MongoDB.Driver;
using ServerApplication.BLL.Models.DB;
using ServerApplication.BLL.RepositoryInterfaces;
using ServerApplication.DATA.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServerApplication.DATA.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : DBModel
    {
        private IMongoDBContext _dbContext;
        public IMongoCollection<TEntity> Collection { get; private set; }

        public BaseRepository(IMongoDBContext dbContext)
        {
            this._dbContext = dbContext;
            Collection = _dbContext.GetCollection<TEntity>();
        }

        public long Count()
        {
            try
            {
                return Collection.EstimatedDocumentCount();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public bool Create(TEntity model)
        {
            try
            {
                Collection.InsertOne(model);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(id.ToString(), out objectId))
            {
                return false;
            }
            var filterId = Builders<TEntity>.Filter.Eq("_id", objectId);
            var deleted = Collection.FindOneAndDelete(filterId);
            return deleted != null;
        }

        public DeleteResult EmptyTable()
        {
            return Collection.DeleteMany(FilterDefinition<TEntity>.Empty);
        }

        public TEntity Find(string id)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(id.ToString(), out objectId))
            {
                return null;
            }
            var filterId = Builders<TEntity>.Filter.Eq("_id", objectId);
            var model = Collection.Find(filterId).FirstOrDefault();
            return model;
        }

        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.Find(filter).ToList();
        }

        public IEnumerable<TEntity> QueryAll()
        {
            return Collection.Find(FilterDefinition<TEntity>.Empty).ToList();
        }

        public bool Update(TEntity model)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(model.Id.ToString(), out objectId))
            {
                return false;
            }
            var filterId = Builders<TEntity>.Filter.Eq("_id", objectId);
            var updated = Collection.FindOneAndReplace(filterId, model);
            return updated != null;
        }
    }
}
