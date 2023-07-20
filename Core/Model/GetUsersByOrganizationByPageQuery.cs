using Core.Model.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
	public class GetUsersByOrganizationByPageQuery : IRequest<IEnumerable<UserDto>>
	{
		public Guid OrganizationId { get; set; }
		public int Index { get; set; }
		public int PageSize { get; set; }
	}
}
