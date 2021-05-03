﻿using Festival.App.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Festival.App.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFactory<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<TService, TImplementation>();

            services.AddSingleton<Func<TService>>(x => x.GetService<TService>);

            services.AddSingleton<IFactory<TService>, Factory<TService>>();
        }
    }
}