using System;
using DeliveryHero.Api.Model;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DeliveryHero.Data
{
    public class DbContext
    {
        private readonly ILogger _logger;

        protected MongoClient Client { get; }
        protected IMongoDatabase Database { get; }

        public IMongoCollection<ShortUrl> ShortUrls
            => Database.GetCollection<ShortUrl>(nameof(ShortUrl));

        public DbContext(
            string connectionString,
            string database,
            ILogger<DbContext> logger)
        {
            _logger = logger;

            _logger.LogInformation("Connection to {@mongoUrl} at {@database}", connectionString, database);

            var mongoUrl = new MongoUrl(connectionString);

            Client = new MongoClient(mongoUrl);
            Database = Client.GetDatabase(database);

            if (!BsonClassMap.IsClassMapRegistered(typeof(ShortUrl)))
                BsonClassMap.RegisterClassMap<ShortUrl>(cm =>
                {
                    cm.MapIdProperty(c => c.Id);
                    cm.MapProperty(c => c.FullUrl);
                });
        }
    }
}
