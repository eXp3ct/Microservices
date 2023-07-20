using Core.Model;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserCreationService.Commands;

namespace UserCreationService.Controllers
{
	[Route("[controller]")]
	public class UsersController : Controller
	{
		public readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}


		[HttpPost]
		public async Task<IActionResult> SendUserAsync([FromBody] SendUserQuery query)
		{
			try
			{
				var user = await _mediator.Send(query);

				return Ok(user);
			}
			catch (ValidationException ex)
			{
				return BadRequest(ex.Errors);
			}
		}
	}
}
