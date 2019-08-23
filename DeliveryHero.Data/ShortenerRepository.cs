using System.Linq;
using DeliveryHero.Api.Model;
using DeliveryHero.Api.Services;
using MongoDB.Driver;

namespace DeliveryHero.Data
{
    public class ShortenerRepository : IShortenerRepository
    {
        private readonly DbContext _dbContext;

        public ShortenerRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(ShortUrl shortUrl)
        {
            try
            {
                _dbContext.ShortUrls.InsertOne(shortUrl);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Exists(ShortUrl candidate)
        {
            var count = _dbContext.ShortUrls
                .CountDocuments(url => url.Id == candidate.Id);

            return count > 0;
        }

        public ShortUrl Get(string id)
        {
            var shortUrl = _dbContext.ShortUrls.FindSync(url => url.Id == id).FirstOrDefault();

            return shortUrl;
        } 

        public ShortUrl[] GetAll()
        {
            var urls = _dbContext.ShortUrls.Find(FilterDefinition<ShortUrl>.Empty).ToList();

            return urls.ToArray();
                
        }
    }
}
