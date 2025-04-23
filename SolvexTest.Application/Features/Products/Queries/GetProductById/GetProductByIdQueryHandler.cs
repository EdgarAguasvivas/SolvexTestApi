using AutoMapper;
using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.Contracts.Persistence;
using SolvexTest.Application.DTOs;
using SolvexTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetAsync(x => x.Id == request.Id,includeString: "ProductVariations");

            if (product == null)
                return null;

            return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product.FirstOrDefault()));
        }
    }
}
