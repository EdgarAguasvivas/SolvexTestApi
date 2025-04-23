using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.DTOs.Auth;
using SolvexTest.Application.Features.Users.Command.CreateUser;
using SolvexTest.Application.Features.Users.Command.DeleteUser;
using SolvexTest.Application.Features.Users.Command.UpdateUser;
using SolvexTest.Application.Features.Users.Queries.GetUsers;
using SolvexTest.Application.Pagination;

namespace SolvexTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Seller,User")]
        public async Task<ActionResult<PaginatedResult<UserDto>>> GetUsers([FromQuery] GetUsersQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { Id = id });
            return result.IsSuccess ? Ok(result) : NotFound();
        }
    }
}
