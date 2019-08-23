using DeliveryHero.Api.Model;

namespace DeliveryHero.Api.Services
{
    public interface IShortenerRepository
    {
        bool Exists(ShortUrl candidate);
        bool Add(ShortUrl shortUrl);
        ShortUrl Get(string id);
        ShortUrl[] GetAll();
    }
}
