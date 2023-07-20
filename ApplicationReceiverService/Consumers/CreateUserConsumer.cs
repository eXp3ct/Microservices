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
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();

			_logger.LogInformation($"User with Id: {user.Id} Has been added to Database");
		}
	}
}
