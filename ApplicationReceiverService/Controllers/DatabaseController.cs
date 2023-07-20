using Core.Model;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationReceiverService.Controllers
{
	[Route("[controller]")]
	public class DatabaseController : Controller
	{
		private readonly IMediator _mediator;

		public DatabaseController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[Route("/attach")]
		public async Task<IActionResult> AttachUserToOrganization([FromBody] AttachUserToOrganizationQuery query)
		{
			await _mediator.Send(query);

			return Ok();
		}

		[HttpPost]
		[Route("/get")]
		public async Task<IActionResult> GetUsersByOrganization([FromBody] GetUsersByOrganizationByPageQuery query)
		{
			try
			{
				var users = await _mediator.Send(query);

				return Ok(users);
			}
			catch (ValidationException ex)
			{
				return BadRequest(ex.Errors);
			}
		}
	}
}
