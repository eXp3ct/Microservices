using AutoMapper;
using Core.Model;
using FluentValidation;
using MassTransit;
using MediatR;

namespace UserCreationService.Commands
{
	public class SendUserQueryHandler : IRequestHandler<SendUserQuery, User>
	{
		private readonly IMapper _mapper;
		private readonly IBus _bus;
		private readonly ILogger _logger;

		public SendUserQueryHandler(IMapper mapper, IBus bus, ILogger<SendUserQueryHandler> logger)
		{
			_mapper = mapper;
			_bus = bus;
			_logger = logger;
		}

		public async Task<User> Handle(SendUserQuery request, CancellationToken cancellationToken)
		{

			var user = _mapper.Map<User>(request);

			try
			{
				await _bus.Publish(user, cancellationToken);
				_logger.LogInformation($"Successfuly send {user}");
			}
			catch
			{
				_logger.LogError($"Failed to send {user}");
				throw;
			}
			return user;

		}
	}
}
