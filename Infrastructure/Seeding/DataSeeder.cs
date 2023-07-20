using Core.Model;
using Core.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Seeding
{
	public class DataSeeder
	{
		private readonly IAppDbContext _context;

		public DataSeeder(IAppDbContext context)
		{
			_context = context;
		}

		public void SeedData()
		{
			if(!_context.Users.Any())
			{
				var users = new List<User>()
				{
					new User()
					{
						Id = Guid.NewGuid(),
						Name = "User1",
						Surname = "User1Surname",
						MiddleName = "user1middlename",
						PhoneNumber = "(717) 550-1675",
						Email = "yanaf-iyigo94@mail.com"
					},
					new User()
					{
						Id = Guid.NewGuid(),
						Name = "User2",
						Surname = "User2Surname",
						MiddleName = "user2middlename",
						PhoneNumber = "(206) 342-8631",
						Email = "rine-voxiye95@aol.com"
					},
					new User()
					{
						Id = Guid.NewGuid(),
						Name = "User3",
						Surname = "User3Surname",
						MiddleName = "",
						PhoneNumber = "(209) 300-2557",
						Email = "cew_iwadini37@outlook.com"
					},
					new User()
					{
						Id = Guid.NewGuid(),
						Name = "User4",
						Surname = "User4Surname",
						MiddleName = "",
						PhoneNumber = "(212) 658-3916",
						Email = "fudub-eluge97@outlook.com"
					},
					new User()
					{
						Id = Guid.NewGuid(),
						Name = "User5",
						Surname = "User5Surname",
						MiddleName = "user5middlename",
						PhoneNumber = "(253) 644-2182",
						Email = "hatuvas_ake38@yahoo.com"
					}
				};

				_context.Users.AddRange(users);
				_context.SaveChangesAsync();
			}

			if (!_context.Organizations.Any())
			{
				var organizations = new List<Organization>()
				{
					new Organization{Id = Guid.NewGuid(), Name = "Organization 1"},
					new Organization{Id = Guid.NewGuid(), Name = "Organization 2"},
					new Organization{Id = Guid.NewGuid(), Name = "Organization 3"},
					new Organization{Id = Guid.NewGuid(), Name = "Organization 4"},
				};
				_context.Organizations.AddRange(organizations);
				_context.SaveChangesAsync();
			}
		}
	}
}
