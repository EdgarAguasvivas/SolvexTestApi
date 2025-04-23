using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolvexTest.Application.Features.ProductVariations.Command.CreateProductVariation;

namespace SolvexTest.Api.Controllers
{
    public class ProductVariationsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductVariationsController(IMediator mediator)
        {
            _mediator = mediator;
        }       

        [HttpPost]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> Create([FromBody] CreateProductVariationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }      
    }
}
