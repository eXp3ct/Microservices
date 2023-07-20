using Core.Model;
using Core.Model.Interfaces;
using MediatR;

namespace ApplicationReceiverService.Commands
{
	public class AttachUserToOrganizationQueryHandler : IRequestHandler<AttachUserToOrganizationQuery>
	{
		private readonly IAppDbContext _context;
		private readonly ILogger _logger;

		public AttachUserToOrganizationQueryHandler(IAppDbContext context, ILogger<AttachUserToOrganizationQueryHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task Handle(AttachUserToOrganizationQuery request, CancellationToken cancellationToken)
		{
			var user = await _context.Users.FindAsync(request.UserId, cancellationToken) 
				?? throw new InvalidOperationException($"User with Id {request.UserId} not found");

			user.OrganizationId = request.OrganizationId;

			_logger.LogInformation($"User {user.Id} attached to organization {user.OrganizationId}");

			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
