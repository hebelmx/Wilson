﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExxerProject.Accounting.Core.Entities;
using ExxerProject.Accounting.Data.Extensions;

namespace ExxerProject.Accounting.Data.Configurations
{
    public class BillTypeConfiguration : EntityTypeConfiguration<Bill>
    {
        public BillTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Bill>());
        }

        public override void Map(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.ProjectId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.InvoiceId).HasMaxLength(36);
            builder.Property(x => x.Amount).HasPrecision(18, 4).IsRequired();
            builder.Property(x => x.HtmlContent).IsRequired();
            builder.HasOne(x => x.Project).WithMany(x => x.Bills).HasForeignKey(x => x.ProjectId);
            builder.HasOne(x => x.Invoice).WithOne(x => x.Bill).HasForeignKey<Bill>(x => x.InvoiceId);
        }
    }
}
