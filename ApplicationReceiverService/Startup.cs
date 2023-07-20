using ApplicationReceiverService.Consumers;
using Core.Behavior;
using Core.Model.Validation;
using FluentValidation;
using Infrastructure;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Persistance.Seeding;
using System.Reflection;
using UserCreationService.BusConfiguration;

namespace ApplicationReceiverService
{
	public class Startup
	{
		public IConfiguration Configuration { get; set; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

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
				x.AddConsumer<CreateUserConsumer>();
				x.UsingRabbitMq((context, cfg) =>
				{
					var rabbitMqConfig = Configuration.GetSection("RabbitMqConfig").Get<RabbitMqConfiguration>();
					cfg.Host(rabbitMqConfig.Host, "/", h =>
					{
						h.Username(rabbitMqConfig.Username);
						h.Password(rabbitMqConfig.Password);
					});

					cfg.ReceiveEndpoint("user-queue", e =>
					{
						e.ConfigureConsumer<CreateUserConsumer>(context);
					});
				});
			});
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
			services.AddValidatorsFromAssemblyContaining<PaginationValidator>();
			services.AddPersistance(Configuration);
			services.AddScoped<DataSeeder>();
		}
	}
}
