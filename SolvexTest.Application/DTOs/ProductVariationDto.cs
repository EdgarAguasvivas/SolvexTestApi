using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.DTOs
{
    public class ProductVariationDto
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}
