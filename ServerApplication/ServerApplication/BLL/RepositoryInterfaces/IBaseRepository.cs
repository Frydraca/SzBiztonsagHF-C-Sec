using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServerApplication.BLL.RepositoryInterfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Find(string id);
        bool Update(TEntity model);
        bool Create(TEntity model);
        bool Delete(string id);
        long Count();
        DeleteResult EmptyTable();
        IEnumerable<TEntity> QueryAll();
        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter);
    }
}
