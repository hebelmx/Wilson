﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExxerProject.Accounting.Core.Entities;

namespace ExxerProject.Accounting.Data.Configurations
{
    public class ItemTypeConfiguration : EntityTypeConfiguration<Item>
    {
        public ItemTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Item>());
        }

        public override void Map(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Name).HasMaxLength(70).IsRequired();
        }
    }
}
