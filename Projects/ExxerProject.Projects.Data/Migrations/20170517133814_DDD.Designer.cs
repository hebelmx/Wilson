﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ExxerProject.Projects.Data;

namespace ExxerProject.Projects.Data.Migrations
{
    [DbContext(typeof(ProjectsDbContext))]
    [Migration("20170517133814_DDD")]
    partial class DDD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("Projects")
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExxerProject.Projects.Core.Entities.Company", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("ExxerProject.Projects.Core.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<bool>("IsFired");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ExxerProject.Projects.Core.Entities.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<DateTime?>("ActualEndDate");

                    b.Property<string>("CustomerId");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("GuaranteePeriodInMonths");

                    b.Property<bool>("IsActive");

                    b.Property<string>("ManagerId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(900);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ExxerProject.Projects.Core.Entities.Project", b =>
                {
                    b.HasOne("ExxerProject.Projects.Core.Entities.Company", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("ExxerProject.Projects.Core.Entities.Employee", "Manager")
                        .WithMany("Projects")
                        .HasForeignKey("ManagerId");
                });
        }
    }
}
