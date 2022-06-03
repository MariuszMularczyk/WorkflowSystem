using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WorkflowSystem.EntityFramework
{
    public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductOrders)
                .IsRequired()
                .HasForeignKey(x => x.ProductId);
        }
    }
}
