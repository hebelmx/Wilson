﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ExxerProject.Scheduler.Data;
using ExxerProject.Scheduler.Core.Enumerations;

namespace ExxerProject.Scheduler.Data.Migrations
{
    [DbContext(typeof(SchedulerDbContext))]
    [Migration("20170515152128_DDD")]
    partial class DDD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("Scheduler")
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExxerProject.Scheduler.Core.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<int>("EmployeePosition");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<bool>("IsFired");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("PayRateId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("PayRateId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ExxerProject.Scheduler.Core.Entities.Paycheck", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<DateTime>("Date");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<int>("ExtraHours");

                    b.Property<DateTime>("From");

                    b.Property<int>("HourOnBusinessTrip");

                    b.Property<int>("HourOnHolidays");

                    b.Property<int>("Hours");

                    b.Property<int>("PaidDaysOff");

                    b.Property<decimal>("PayBusinessTrip")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PayForExtraHours")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PayForHolidayHours")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PayForHours")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PayForPayedDaysOff")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("SickDaysOff");

                    b.Property<DateTime>("To");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("UnpaidDaysOff");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Paychecks");
                });

            modelBuilder.Entity("ExxerProject.Scheduler.Core.Entities.PayRate", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<decimal>("BusinessTripHour")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("ExtraHour")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("HoidayHour")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Hour")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("IsBaseRate");

                    b.HasKey("Id");

                    b.ToTable("PayRates");
                });

            modelBuilder.Entity("ExxerProject.Scheduler.Core.Entities.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(900);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ExxerProject.Scheduler.Core.Entities.Schedule", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<DateTime>("Date");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<int>("ExtraWorkHours");

                    b.Property<string>("ProjectId")
                        .HasMaxLength(36);

                    b.Property<int>("ScheduleOption");

                    b.Property<int>("WorkHours");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("ExxerProject.Scheduler.Core.Entities.Employee", b =>
                {
                    b.HasOne("ExxerProject.Scheduler.Core.Entities.PayRate", "PayRate")
                        .WithMany()
                        .HasForeignKey("PayRateId");
                });

            modelBuilder.Entity("ExxerProject.Scheduler.Core.Entities.Paycheck", b =>
                {
                    b.HasOne("ExxerProject.Scheduler.Core.Entities.Employee", "Employee")
                        .WithMany("Paychecks")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ExxerProject.Scheduler.Core.Entities.Schedule", b =>
                {
                    b.HasOne("ExxerProject.Scheduler.Core.Entities.Employee", "Employee")
                        .WithMany("Schedules")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ExxerProject.Scheduler.Core.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });
        }
    }
}
