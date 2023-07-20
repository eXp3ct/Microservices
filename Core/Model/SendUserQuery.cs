using Core.Model;
using MediatR;

namespace Core.Model
{
	public class SendUserQuery : IRequest<User>
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string? MiddleName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
	}
}
