
using Persistance.Seeding;
using Serilog;

namespace ApplicationReceiverService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using var scope = host.Services.CreateScope();
			var services = scope.ServiceProvider;
			var seeder = services.GetRequiredService<DataSeeder>();
			seeder.SeedData();

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.UseSerilog((hostingContext, loggerConfiguration) =>
				{
					loggerConfiguration
						.MinimumLevel.Information()
						.WriteTo.Console();
				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
		}
	}
}