using Core.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
		{
			//var connectionString = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<AppDbContext>();
			services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

			return services;
		}
	}
}
