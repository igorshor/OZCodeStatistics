using System;
using System.Threading.Tasks;
using OzCodeStatitics.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace OzCodeStatitics.MongoDb
{
    public class MongoDbAccessLeyer : IDataAccessLeyer
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public MongoDbAccessLeyer(string connectionString, string dbName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(dbName);
        }

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public RepositoryStatistics Get()
        {
            throw new NotImplementedException();
        }

        public async Task Add(RepositoryStatistics item)
        {
            var document = new BsonDocument
            {
                //{ "linq", item}
            };

            var collection = _database.GetCollection<BsonDocument>("statistics");
            await collection.InsertOneAsync(document);
        }
    }
}
