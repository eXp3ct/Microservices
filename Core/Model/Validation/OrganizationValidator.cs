using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.Validation
{
	public class OrganizationValidator : AbstractValidator<Organization>
	{
        public OrganizationValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(255);
        }
    }
}
