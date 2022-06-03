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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(x => x.ProductOrders)
                .WithOne(x => x.Order)
                .IsRequired()
                .HasForeignKey(x => x.OrderId);
        }
    }
}
