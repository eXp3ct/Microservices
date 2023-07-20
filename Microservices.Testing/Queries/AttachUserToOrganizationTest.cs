using ApplicationReceiverService.Commands;
using Core.Model;
using Core.Model.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserCreationService.Commands;

namespace Microservices.Testing.Queries
{
	public class AttachUserToOrganizationTest
	{
		[Fact]
		public async Task AttachUserToOrganizationTest_Success()
		{
			var serviceProvider = new ServiceCollection()
				.AddLogging(builder => builder.AddSerilog())
				.AddDbContext<AppDbContext>(options =>
				{
					var connectionString = "Host=localhost;Port=5432;Database=admin;Username=admin;Password=admin;";
					options.UseNpgsql(connectionString).EnableSensitiveDataLogging();
				}, ServiceLifetime.Scoped, ServiceLifetime.Scoped)
				.AddScoped<IAppDbContext, AppDbContext>()
				.BuildServiceProvider();

			var logger = serviceProvider.GetRequiredService<ILogger<AttachUserToOrganizationQueryHandler>>();
			var context = serviceProvider.GetRequiredService<AppDbContext>();
			context.Database.EnsureCreated();

			var user = new User()
			{
				Id = Guid.NewGuid(),
				Name = "TestUser",
				Surname = "TestUserSurname",
				MiddleName = "TestUserMiddlename",
				PhoneNumber = "+79641546116",
				Email = "email@email.com"
			};
			var organization = new Organization()
			{
				Id = Guid.NewGuid(),
				Name = "Test org"
			};
			await context.Users.AddAsync(user);
			await context.Organizations.AddAsync(organization);
			await context.SaveChangesAsync();

			var query = new AttachUserToOrganizationQuery()
			{
				UserId = user.Id,
				OrganizationId = organization.Id,
			};
			var handler = new AttachUserToOrganizationQueryHandler(context, logger);
			await handler.Handle(query, CancellationToken.None);

			var createdUser = await context.Users.FindAsync(user.Id, CancellationToken.None);
			Assert.True(createdUser.OrganizationId == organization.Id);
		}
	}
}
