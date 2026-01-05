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
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.InvoiceNumber)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(x => x.Date)
                   .IsRequired();

            builder.HasMany(x => x.Items)
                   .WithOne(i => i.Invoice)
                   .HasForeignKey(i => i.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
