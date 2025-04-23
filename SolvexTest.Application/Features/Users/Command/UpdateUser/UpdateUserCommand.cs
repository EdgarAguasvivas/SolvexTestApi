using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.Users.Command.UpdateUser
{
    public class UpdateUserCommand : IRequest<Result<UserDto>>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
