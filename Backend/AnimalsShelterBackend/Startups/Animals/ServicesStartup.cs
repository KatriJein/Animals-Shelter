using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Services.Animals;
using Core.Base.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Startups.Animals
{
    public static class ServicesStartup
    {
        public static IServiceCollection AddAnimalsServices(this IServiceCollection services)
        {
            services.AddScoped<IAnimalsService, AnimalsService>();
            return services;
        }
    }
}
