namespace Core.Model.Dtos
{
	public class UserDto : EntityBase
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
	}
}
