using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Domain.Animals.Repositories;
using AnimalsShelterBackend.Infrastructure.Animals.Repositories;
using Core.Base.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Startups.Animals
{
    public static class DomainStartup
    {
        public static IServiceCollection AddAnimalsDomain(this IServiceCollection services)
        {
            services.AddScoped<IAnimalsRepository, AnimalsRepository>();
            return services;
        }
    }
}
