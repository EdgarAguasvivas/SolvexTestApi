using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SolvexTest.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolvexTest.Domain.Entities;
using System.Reflection.Emit;

namespace SolvexTest.Infrastructure.Configuration
{
    public class ProductVariationConfiguration : IEntityTypeConfiguration<ProductVariation>
    {
        public void Configure(EntityTypeBuilder<ProductVariation> builder)
        {
            builder.ToTable(nameof(ProductVariation), "dbo");
            builder.HasKey(u => u.Id);

            builder.HasOne(pv => pv.Product)
                 .WithMany(p => p.ProductVariations)
                 .HasForeignKey(pv => pv.ProductId);
        }
    }
}
