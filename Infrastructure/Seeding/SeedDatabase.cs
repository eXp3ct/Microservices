using Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeding
{
	public class SeedDatabase
	{
		public static void Seed(ModelBuilder modelBuilder)
		{
			var user = new User
			{
				Id = Guid.NewGuid(),
				Name = "Alex",
				Surname = "Alex1",
				MiddleName = "",
				PhoneNumber = "1234567890",
				Email = "mail@mail.com",
			};
			var organization = new Organization
			{
				Id = Guid.NewGuid(),
				Name = "Organization A"
			};
			user.OrganizationId = organization.Id;
			
			modelBuilder.Entity<Organization>().HasData(organization);
			modelBuilder.Entity<User>().HasData(user);
		}
	}
}
