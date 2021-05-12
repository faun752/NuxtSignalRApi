using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NuxtSignalRApi.Models;

namespace NuxtSignalRApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSignalRConfiguration (this IServiceCollection services, IConfiguration configuration, Settings settings)
        {
            if (settings.UseSignalRBackplane)
            {
                services.AddSignalR ().AddStackExchangeRedis (settings.RedisConnectionString);
            }
            else
            {
                services.AddSignalR ();
            }
        }
    }
}