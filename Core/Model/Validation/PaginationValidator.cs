using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.Validation
{
	public class PaginationValidator : AbstractValidator<GetUsersByOrganizationByPageQuery>
	{
        public PaginationValidator()
        {
            RuleFor(x => x.Index).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageSize).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
