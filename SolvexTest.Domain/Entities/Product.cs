using SolvexTest.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Domain.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProductVariation> ProductVariations { get; set; } = new List<ProductVariation>();
    }
}
