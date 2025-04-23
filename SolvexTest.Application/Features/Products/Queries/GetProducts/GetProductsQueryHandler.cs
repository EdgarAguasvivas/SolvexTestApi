using AutoMapper;
using MediatR;
using SolvexTest.Application.Contracts.Persistence;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.Pagination;
using SolvexTest.Domain.Entities;

namespace SolvexTest.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedResult<ProductDto>>
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IMapper mapper;

        public GetProductsQueryHandler(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var allProducts = await productRepository.GetAsync(
        predicate: string.IsNullOrWhiteSpace(request.SearchTerm)
            ? null
            : p => p.Name.Contains(request.SearchTerm),
        includeString: "ProductVariations"
    );
            var totalItems = allProducts.Count;
            allProducts = allProducts.OrderByDescending(x => x.CreatedAt).ToList();

            var pagedItems = allProducts
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var mappedItems = mapper.Map<List<ProductDto>>(pagedItems);

            return PaginatedResult<ProductDto>.Success(mappedItems, totalItems, request.Page, request.PageSize);
        }
    }
}
