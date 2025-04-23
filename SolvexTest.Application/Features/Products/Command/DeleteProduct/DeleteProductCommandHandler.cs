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

namespace SolvexTest.Application.Features.Products.Command.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<ProductDto>>
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IMapper mapper;

        public DeleteProductCommandHandler(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id);
            await productRepository.DeleteAsync(product);
            return Result<ProductDto>.Success(mapper.Map<ProductDto>(request),"Producto eliminado correctamente");
        }
    }
}
