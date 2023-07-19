using Core.Model;
using Core.Model.Configuration;
using Core.Model.Interfaces;
using Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure
{
	public class AppDbContext : DbContext, IAppDbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Organization> Organizations { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionString = "Host=postgresql;Port=5432;Database=postgres;Username=admin;Password=admin;";
			optionsBuilder.UseNpgsql(connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new OrganizationConfiguration());

			SeedDatabase.Seed(modelBuilder);
		}
	}
}

