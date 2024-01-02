using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnionArch.Application.Features.Products.Queries.GetAllProducts;

namespace OnionArch.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TestController(IMediator mediator)
		{
			_mediator = mediator;
		}


		[HttpGet]
		public async Task<ActionResult> GetAllProductsAsync()
		{
			var response = await _mediator.Send(new GetAllProductsQueryRequest());
			return Ok(response);
		}
	}
}
