using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolvexTest.Application.Features.Auth.Users.Commands.RegisterUser;
using SolvexTest.Application.Features.Auth.Users.Queries.Login;

namespace SolvexTest.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginQuery query)
        {
            var user = await _mediator.Send(query);
            return Ok(user);
        }
    }
}