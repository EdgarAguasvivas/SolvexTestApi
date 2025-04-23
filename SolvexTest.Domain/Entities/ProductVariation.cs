using SolvexTest.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Domain.Entities
{
    public class ProductVariation : BaseEntity
    {
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
