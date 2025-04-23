using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.Features.Products.Command.CreateProduct;
using SolvexTest.Application.Features.Products.Command.DeleteProduct;
using SolvexTest.Application.Features.Products.Command.UpdateProduct;
using SolvexTest.Application.Features.Products.Queries.GetProductById;
using SolvexTest.Application.Features.Products.Queries.GetProducts;
using SolvexTest.Application.Pagination;

namespace SolvexTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Seller,User")]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetProducts([FromQuery] GetProductsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Seller,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand { Id = id });
            return result.IsSuccess ? Ok(result) : NotFound();
        }
    }
}
