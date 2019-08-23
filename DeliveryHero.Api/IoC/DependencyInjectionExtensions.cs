using System;
using DeliveryHero.Api.Services;
using DeliveryHero.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DeliveryHero.Api.IoC
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterDeliveryHeroServices(this IServiceCollection services)
        {
            return services
                .AddScoped(ctx => 
                {
                    var logger = ctx.GetService<ILoggerFactory>().CreateLogger<DbContext>();
                    return new DbContext(Environment.GetEnvironmentVariable("MONGO_URL"), "ShortUrl", logger);
                })
                .AddScoped<IShortenerRepository, ShortenerRepository>()
                .AddScoped<IShortenerService, ShortenerService>();
        }
    }
}
