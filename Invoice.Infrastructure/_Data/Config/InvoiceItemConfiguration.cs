using InvoiceSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Infrastructure._Data.Config
{
    internal class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ItemCode)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.Qty).IsRequired();

            builder.Property(x => x.Price)
                   .IsRequired()
                   .HasColumnType(typeName: "decimal(18,2)");



            builder.Ignore(x => x.Total);


        }
    }
}
