using Core.Model;
using Core.Model.Interfaces;
using MassTransit;

namespace ApplicationReceiverService.Consumers
{
	public class CreateUserConsumer : IConsumer<User>
	{
		private readonly IAppDbContext _context;
		private readonly ILogger _logger;

		public CreateUserConsumer(IAppDbContext context, ILogger<CreateUserConsumer> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<User> context)
		{
			var user = context.Message;
			//user.OrganizationId = Guid.Parse("ba02601a-2475-430d-be0e-f03359fe7c5a");
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();

			_logger.LogInformation($"\nUser with Id: {user.Id} Has been added to Database");
		}
	}
}
