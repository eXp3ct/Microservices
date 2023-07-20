using ApplicationReceiverService.Consumers;
using AutoMapper;
using Core.Model;
using Core.Model.Interfaces;
using Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;
using UserCreationService.Commands;

namespace Microservices.Testing.Queries
{
	public class CreateUserCommandHandlerTest 
	{
		[Fact]
		public async Task CreateUserCommandHanlder_Success()
		{
			//Arrange
			var serviceProvider = new ServiceCollection()
				.AddAutoMapper(Assembly.GetExecutingAssembly())
				.AddLogging(builder => builder.AddSerilog())
				.AddDbContext<AppDbContext>(options =>
				{
					var connectionString = "Host=localhost;Port=5432;Database=admin;Username=admin;Password=admin;";
					options.UseNpgsql(connectionString).EnableSensitiveDataLogging();
				}, ServiceLifetime.Scoped, ServiceLifetime.Scoped)
				.AddScoped<IAppDbContext, AppDbContext>()
				.AddMassTransit(x =>
				{
					x.AddConsumer<CreateUserConsumer>();
					x.UsingInMemory((context, cfg) =>
					{
						cfg.ReceiveEndpoint("user-queue", e =>
						{
							e.ConfigureConsumer<CreateUserConsumer>(context);
						});
					});
				})
				.BuildServiceProvider();

			var mapper = serviceProvider.GetRequiredService<IMapper>();
			var logger = serviceProvider.GetRequiredService<ILogger<SendUserQueryHandler>>();
			var bus = serviceProvider.GetRequiredService<IBus>();
			var busControl = serviceProvider.GetRequiredService<IBusControl>();
			await busControl.StartAsync();
			var context = serviceProvider.GetRequiredService<AppDbContext>();
			context.Database.EnsureCreated();
			var hanlder = new SendUserQueryHandler(mapper, bus, logger);

			var userInfo = new SendUserQuery()
			{
				Name = "TestUser",
				Surname = "TestUserSurname",
				MiddleName = "TestUserMiddlename",
				PhoneNumber = "+79641546116",
				Email = "email@email.com"
			};

			Log.Information("Sending user information to the bus: {UserInfo}", userInfo);
			var user = await hanlder.Handle(userInfo, CancellationToken.None);

			// Assert
			Assert.NotNull(user);
			Assert.True(user.Name == userInfo.Name && user.MiddleName == userInfo.MiddleName
				&& user.Surname == userInfo.Surname && user.PhoneNumber == userInfo.PhoneNumber
				&& user.Email == userInfo.Email);

			await busControl.StopAsync();

			// Проверяем, что пользователь был создан в базе данных
			var createdUser = await context.Users.SingleOrDefaultAsync(u => u.Id == user.Id
				&& u.Name == user.Name
				&& u.Surname == user.Surname
				&& u.MiddleName == user.MiddleName
				&& u.PhoneNumber == user.PhoneNumber
				&& u.Email == user.Email);

			Assert.NotNull(createdUser);
		}
	}
}
