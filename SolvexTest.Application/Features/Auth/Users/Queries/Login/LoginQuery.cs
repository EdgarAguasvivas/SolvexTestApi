using MediatR;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.DTOs.Auth;

namespace SolvexTest.Application.Features.Auth.Users.Queries.Login
{
    public class LoginQuery : IRequest<AuthenticationResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public string? Token { get; set; }
        public UserDto User { get; set; }
        public string? Error { get; set; }
    }
}
