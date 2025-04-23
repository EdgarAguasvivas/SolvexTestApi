using MediatR;
using SolvexTest.Application.Common.Results;
using SolvexTest.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Features.Products.Command.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Result<ProductDto>>
    {
        public int Id { get; set; }
    }
}
