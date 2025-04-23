using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.DTOs;
using SolvexTest.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Result<ProductDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductVariationDto> Variations { get; set; } = new List<ProductVariationDto>();
    }
}
