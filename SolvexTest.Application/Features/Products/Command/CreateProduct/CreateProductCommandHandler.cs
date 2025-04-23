using AutoMapper;
using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.Contracts.Persistence;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.DTOs.Auth;
using SolvexTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductVariation> productVariationRepository;
        private readonly IMapper mapper;

        public CreateProductCommandHandler(IGenericRepository<Product> productRepository, IGenericRepository<ProductVariation> productVariationRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.productVariationRepository = productVariationRepository;
            this.mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await productRepository.GetWhereAsync(u => u.Name == request.Name);
                if (existingProduct.Count != 0)                
                    throw new ApplicationException($"El producto '{request.Name}' ya existe.");
                

                var product = new Product
            {
                Name = request.Name,
                Description = request.Description
            };

            var productResult = await productRepository.AddAsync(product);

            foreach (var item in request.Variations)
            {
                var productVariations = new ProductVariation
                {
                    Color = item.Color,
                    Price = item.Price,
                    ProductId = productResult.Id,
                    ImageUrl = item.ImageUrl
                };

                await productVariationRepository.AddAsync(productVariations);
            }

            return Result<ProductDto>.Success(mapper.Map<ProductDto>(product), "Producto creado correctamente");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
