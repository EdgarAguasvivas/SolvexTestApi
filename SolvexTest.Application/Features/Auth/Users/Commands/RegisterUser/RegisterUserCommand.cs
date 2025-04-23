using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.DTOs.Auth;

namespace SolvexTest.Application.Features.Auth.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Result<UserDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}