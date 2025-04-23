using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SolvexTest.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolvexTest.Domain.Entities;

namespace SolvexTest.Infrastructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product), "dbo");
            builder.HasKey(u => u.Id);

            builder.HasMany(d => d.ProductVariations)
           .WithOne(p => p.Product)
           .HasForeignKey(d => d.ProductId);
        }
    }
}
