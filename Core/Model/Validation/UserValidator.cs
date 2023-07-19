using FluentValidation;

namespace Core.Model.Validation
{
	public class UserValidator : AbstractValidator<User>
	{
		public UserValidator()
		{
			RuleFor(x => x.Id).NotNull();
			RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(64);
			RuleFor(x => x.Surname).NotNull().NotEmpty().MaximumLength(64);
			RuleFor(x => x.MiddleName).MaximumLength(64);
			RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().MaximumLength(64).Matches("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$");
			RuleFor(x => x.Email).NotEmpty().NotNull().MaximumLength(64).EmailAddress();
		}
	}
}
