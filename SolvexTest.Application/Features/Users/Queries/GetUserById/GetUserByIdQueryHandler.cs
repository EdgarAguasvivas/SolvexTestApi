using AutoMapper;
using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.Contracts.Persistence;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.DTOs.Auth;
using SolvexTest.Application.Features.Products.Queries.GetProductById;
using SolvexTest.Domain.Entities;
using SolvexTest.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IGenericRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(x => x.Id == request.Id);

            if (user == null)
                return null;

            return Result<UserDto>.Success(_mapper.Map<UserDto>(user.FirstOrDefault()));
        }
    }
}
