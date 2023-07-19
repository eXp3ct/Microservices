using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
	public class User : EntityBase
	{
		public Guid? OrganizationId { get; set; } = null;
		public string Name { get; set; }
		public string Surname { get; set; }
		public string? MiddleName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }

		public override string ToString()
		{
			return $"\n{Name}\n{Surname}\n{MiddleName}\n{PhoneNumber}\n{Email}\n";
		}
	}
}
