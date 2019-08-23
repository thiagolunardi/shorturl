using DeliveryHero.Api.Model;

namespace DeliveryHero.Api.Services
{
    public interface IShortenerService
    {
        ShortUrl Generate(string fullUrl);
        ShortUrl Get(string id);
        ShortUrl[] GetAll();
    }
}