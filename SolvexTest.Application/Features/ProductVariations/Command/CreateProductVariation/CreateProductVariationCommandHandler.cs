using AutoMapper;
using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.Contracts.Persistence;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.Features.Products.Command.CreateProduct;
using SolvexTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.ProductVariations.Command.CreateProductVariation
{
    public class CreateProductVariationCommandHandler : IRequestHandler<CreateProductVariationCommand, Result<ProductVariationDto>>
    {
        private readonly IGenericRepository<ProductVariation> productVariationRepository;
        private readonly IMapper mapper;

        public CreateProductVariationCommandHandler(IGenericRepository<ProductVariation> productVariationRepository, IMapper mapper)
        {
            this.productVariationRepository = productVariationRepository;
            this.mapper = mapper;
        }

        public async Task<Result<ProductVariationDto>> Handle(CreateProductVariationCommand request, CancellationToken cancellationToken)
        {
            var productVariation = new ProductVariation
            {
                ProductId = request.ProductId,
                Color = request.Color,
                Price = request.Price
            };

            await productVariationRepository.AddAsync(productVariation);
            return Result<ProductVariationDto>.Success(mapper.Map<ProductVariationDto>(productVariation), "Variacion de producto creado correctamente");
        }
    }
}
