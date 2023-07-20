using ApplicationReceiverService.Commands;
using AutoMapper;
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

namespace Microservices.Testing.Queries
{
	public class GetUsersByOrganizationQueryTest
	{
		private readonly Guid OrganizationId = Guid.NewGuid();
		[Fact]
		public async Task GetUsersByOrganizationQuery()
		{
			var serviceProvider = new ServiceCollection()
				.AddAutoMapper(Assembly.GetExecutingAssembly())
				.AddLogging(builder => builder.AddSerilog())
				.AddDbContext<AppDbContext>(options =>
				{
					var connectionString = "Host=localhost;Port=5432;Database=admin;Username=admin;Password=admin;";
					options.UseNpgsql(connectionString).EnableSensitiveDataLogging();
				}, ServiceLifetime.Scoped, ServiceLifetime.Scoped)
				.AddScoped<IAppDbContext, AppDbContext>()
				.BuildServiceProvider();

			var mapper = serviceProvider.GetRequiredService<IMapper>();
			var logger = serviceProvider.GetRequiredService<ILogger<GetUsersByOrganizationByPageQueryHandler>>();
			var context = serviceProvider.GetRequiredService<AppDbContext>();
			context.Database.EnsureCreated();

			await context.Users.AddRangeAsync(CreateUsers());
			var handler = new GetUsersByOrganizationByPageQueryHandler(logger, context, mapper);
			var request = new GetUsersByOrganizationByPageQuery
			{
				OrganizationId = OrganizationId,
				Index = 0,
				PageSize = 5
			};


			var dtos = await handler.Handle(request, CancellationToken.None);
			var users = await context.Users.ToListAsync(CancellationToken.None);
			Assert.All(dtos, dto =>
			{
				var user = users.FirstOrDefault(u => u.Id == dto.Id);
				Assert.NotNull(user);

				Assert.Equal(user.Name, dto.Name);
				Assert.Equal(user.Surname, dto.Surname);
				Assert.Equal(user.PhoneNumber, dto.PhoneNumber);
				Assert.Equal(user.Email, dto.Email);
				// Add more assertions for other properties if needed
			});

		}

		private IEnumerable<User> CreateUsers()
		{
			return new List<User>()
				{
					new User()
					{
						Id = Guid.NewGuid(),
						Name = "User1",
						Surname = "User1Surname",
						MiddleName = "user1middlename",
						PhoneNumber = "(717) 550-1675",
						Email = "yanaf-iyigo94@mail.com",
						OrganizationId = this.OrganizationId
					},
					new User()
					{
						Id = Guid.NewGuid(),
						Name = "User2",
						Surname = "User2Surname",
						MiddleName = "user2middlename",
						PhoneNumber = "(206) 342-8631",
						Email = "rine-voxiye95@aol.com",
						OrganizationId = this.OrganizationId
					},
					new User()
					{
						Id = Guid.NewGuid(),
						Name = "User3",
						Surname = "User3Surname",
						MiddleName = "",
						PhoneNumber = "(209) 300-2557",
						Email = "cew_iwadini37@outlook.com",
						OrganizationId = this.OrganizationId
					},
					new User()
					{
						Id = Guid.NewGuid(),
						Name = "User4",
						Surname = "User4Surname",
						MiddleName = "",
						PhoneNumber = "(212) 658-3916",
						Email = "fudub-eluge97@outlook.com",
						OrganizationId = this.OrganizationId
					},
					new User()
					{
						Id = Guid.NewGuid(),
						Name = "User5",
						Surname = "User5Surname",
						MiddleName = "user5middlename",
						PhoneNumber = "(253) 644-2182",
						Email = "hatuvas_ake38@yahoo.com",
						OrganizationId = this.OrganizationId
					}
				};
		}
	}
}
