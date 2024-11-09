using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Domain.Articles;
using AnimalsShelterBackend.Domain.ShelterUser;
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
		public DbSet<Article> Articles { get; set; }
		public DbSet<User> Users { get; set; }

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
