using AnimalsShelterBackend.Domain.Animals;
using Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace AnimalsShelterBackend.Infrastructure
{
	public class ShelterAppContext : DbContext
	{
		private readonly IOptions<ShelterAppDbContextOptions> _options;

		public DbSet<Animal> Animals { get; set; }

		public ShelterAppContext(DbContextOptions<ShelterAppContext> options, IOptions<ShelterAppDbContextOptions> conOptions)
			: base(options)
		{
			_options = conOptions;
			Database.Migrate();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(_options.Value.ConnectionString, opt => opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
			base.OnConfiguring(optionsBuilder);
		}
	}
}
