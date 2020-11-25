using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ServerApplication.BLL.Models.DB;
using ServerApplication.DATA.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ServerApplication.DATA.Context
{
    public class MongoDBContext : IMongoDBContext
    {
        public MongoClient MongoDBClient { get; set; }
        public IMongoDatabase Database { get; set; }

        public MongoDBContext(IConfiguration configuration)
        {
            MongoDBClient = new MongoClient(configuration.GetSection("MongoSettings").GetSection("Connection").Value);

            Database = MongoDBClient.GetDatabase(configuration.GetSection("MongoSettings").GetSection("DatabaseName").Value);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : DBModel
        {
            var table = typeof(TEntity).GetCustomAttribute<TableAttribute>(false).Name;
            return Database.GetCollection<TEntity>(table);
        }
    }
}
