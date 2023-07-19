using Core.Model.Validation;
using FluentValidation;
using MassTransit;
using System.Reflection;
using UserCreationService.BusConfiguration;

namespace UserCreationService
{
	public class Startup
	{

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public IConfiguration Configuration { get; }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
		{
			if (environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMassTransit(x =>
			{
				x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
				{
					var rabbitMqConfig = Configuration.GetSection("RabbitMqConfig").Get<RabbitMqConfiguration>();

					cfg.Host(rabbitMqConfig.Host, "/", h =>
					{
						h.Username(rabbitMqConfig.Username);
						h.Password(rabbitMqConfig.Password);
					});
					
				}));
			});
			services.AddMassTransitHostedService();
			services.AddValidatorsFromAssemblyContaining<UserValidator>();
			services.AddValidatorsFromAssemblyContaining<OrganizationValidator>();
		}
	}
}
