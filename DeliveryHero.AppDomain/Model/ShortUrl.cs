using System;
using System.Linq;

namespace DeliveryHero.Api.Model
{
    public class ShortUrl
    {
        public ShortUrl(string fullUrl)
        {
            if (string.IsNullOrEmpty(fullUrl)) throw new ArgumentException("Invalid FullUrl", nameof(fullUrl));

            Id = RandomString(6);
            FullUrl = fullUrl;
        }

        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuwxyz";
            var rnd = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public string Id { get; protected set; }
        public string FullUrl { get; protected set; }
    }
}
