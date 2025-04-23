using AutoMapper;
using MediatR;
using SolvexTest.Application.Contracts.Persistence;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.DTOs.Auth;
using SolvexTest.Application.Features.Products.Queries.GetProducts;
using SolvexTest.Application.Pagination;
using SolvexTest.Domain.Entities;
using SolvexTest.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedResult<UserDto>>
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IMapper mapper;

        public GetUsersQueryHandler(IGenericRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<PaginatedResult<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var allProducts = await userRepository.GetAsync(
        predicate: string.IsNullOrWhiteSpace(request.SearchTerm)
            ? null
            : p => p.Username.Contains(request.SearchTerm)
    );
            var totalItems = allProducts.Count;

            var pagedItems = allProducts
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var mappedItems = mapper.Map<List<UserDto>>(pagedItems);

            return PaginatedResult<UserDto>.Success(mappedItems, totalItems, request.Page, request.PageSize);
        }
    }
}