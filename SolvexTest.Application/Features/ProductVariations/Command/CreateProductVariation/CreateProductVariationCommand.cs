using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.ProductVariations.Command.CreateProductVariation
{
    public class CreateProductVariationCommand : IRequest<Result<ProductVariationDto>>
    {
        public int ProductId { get; set; }
        public string Color { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
