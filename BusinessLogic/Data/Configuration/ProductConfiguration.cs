using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Price).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Stock).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(c => c.Category).WithMany().HasForeignKey(p => p.CategoryId);
            builder.HasOne(s => s.State).WithMany().HasForeignKey(p => p.StateId);
        }
    }
}
