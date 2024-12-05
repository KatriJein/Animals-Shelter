using AnimalsShelterBackend.Domain.Feedbacks;
using AnimalsShelterBackend.Infrastructure.Infrastructure.Feedbacks;
using AnimalsShelterBackend.Services.Feedbacks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Startups.Feedbacks
{
	public static class Startup
	{
		public static IServiceCollection AddFeedbacks(this IServiceCollection services)
		{
			services.AddScoped<IFeedbacksRepository, FeedbacksRepository>();
			services.AddScoped<IFeedbacksService, FeedbacksService>();
			return services;
		}
	}
}
