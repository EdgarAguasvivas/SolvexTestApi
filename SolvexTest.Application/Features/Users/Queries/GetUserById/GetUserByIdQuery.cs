using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Result<UserDto>>
    {
        public int Id { get; set; }
    }
}
