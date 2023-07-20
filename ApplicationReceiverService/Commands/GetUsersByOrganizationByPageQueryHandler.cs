using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Model;
using Core.Model.Dtos;
using Core.Model.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationReceiverService.Commands
{
	public class GetUsersByOrganizationByPageQueryHandler : IRequestHandler<GetUsersByOrganizationByPageQuery, IEnumerable<UserDto>>
	{
		private readonly ILogger _logger;
		private readonly IAppDbContext _context;
		private readonly IMapper _mapper;

		public GetUsersByOrganizationByPageQueryHandler(ILogger<GetUsersByOrganizationByPageQueryHandler> logger, IAppDbContext context, IMapper mapper)
		{
			_logger = logger;
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<UserDto>> Handle(GetUsersByOrganizationByPageQuery request, CancellationToken cancellationToken)
		{
			var users = await _context.Users
				.Where(u => u.OrganizationId == request.OrganizationId)
				.Skip(request.Index * request.PageSize)
				.Take(request.PageSize)
				.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);

			_logger.LogInformation($"Get {users.Count} by organization {request.OrganizationId}");

			return users;
		}
	}
}
