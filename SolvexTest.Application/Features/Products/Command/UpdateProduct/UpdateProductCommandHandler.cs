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

namespace SolvexTest.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductDto>>
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductVariation> productVariationRepository;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IGenericRepository<Product> productRepository, IGenericRepository<ProductVariation> productVariationRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.productVariationRepository = productVariationRepository;
            this.mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var product = await productRepository.GetAsync(x => x.Id == request.Id, includeString: "ProductVariations");
                var productEntity = product.FirstOrDefault();

                if (product == null)
                    throw new ApplicationException($"Producto con el ID {request.Id} no existe");

                var variationIds = request.Variations
                    .Where(v => v.Id != 0)
                    .Select(v => v.Id);

                var variationsToRemove = productEntity.ProductVariations
                    .Where(v => !variationIds.Contains(v.Id))
                    .ToList();

                foreach (var variation in variationsToRemove)
                {
                    await productVariationRepository.DeleteAsync(variation);
                }

                foreach (var variationDto in request.Variations)
                {
                    if (variationDto.Id == 0)
                    {
                        variationDto.ProductId = productEntity.Id;
                        await productVariationRepository.AddAsync(mapper.Map<ProductVariation>(variationDto));
                    }
                    else
                    {
                        var existingVariation = productEntity.ProductVariations
                            .FirstOrDefault(v => v.Id == variationDto.Id);

                        if (existingVariation != null)
                        {
                            mapper.Map(variationDto, existingVariation);
                        }
                    }
                }

                await productRepository.UpdateAsync(productEntity);
                return Result<ProductDto>.Success(mapper.Map<ProductDto>(productEntity), "Producto actualizado correctamente");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
