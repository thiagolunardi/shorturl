using System;
using DeliveryHero.Api.Model;
using Microsoft.Extensions.Logging;

namespace DeliveryHero.Api.Services
{
    public class ShortenerService : IShortenerService
    {
        private readonly IShortenerRepository _shortenerRepository;
        private readonly ILogger _logger;

        public ShortenerService(
            IShortenerRepository shortenerRepository,
            ILogger<ShortenerService> logger)
        {
            _shortenerRepository = shortenerRepository;
            _logger = logger;
        }

        public ShortUrl Generate(string fullUrl)
        {
            try
            {
                ShortUrl shortUrl;
                while (true)
                {
                    shortUrl = new ShortUrl(fullUrl);
                    if (_shortenerRepository.Exists(shortUrl)) continue;
                    break;
                }

                _shortenerRepository.Add(shortUrl);

                _logger.LogInformation("New {@shortUrl} created.", shortUrl);

                return shortUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new {@shortUrl}", fullUrl);

                throw;
            }
        }

        public ShortUrl Get(string id)
        {
            try
            {
                var url = _shortenerRepository.Get(id);

                if (url is null)
                    _logger.LogInformation("Short URL for {@id} found.", id);
                else
                    _logger.LogWarning("Short URL for {@id} not found.", id);

                return url;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting URL for {@id}", id);

                throw;
            }
        }

        public ShortUrl[] GetAll()
        {
            try
            {
                var urls = _shortenerRepository.GetAll();

            _logger.LogInformation("Listing all {@total} shortened URL.", urls.Length);

            return urls;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all shortened URLs");

                throw;
            }
        }
    }
}
