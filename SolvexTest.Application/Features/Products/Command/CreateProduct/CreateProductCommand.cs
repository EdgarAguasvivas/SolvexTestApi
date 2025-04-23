using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommand : IRequest<Result<ProductDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductVariationDto> Variations { get; set; }
    }
}
