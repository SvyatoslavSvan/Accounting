﻿// <auto-generated />
using System;
using Accounting.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Accounting.DAL.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Accounting.Domain.Models.Accrual", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Ammount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Accruals");
                });

            modelBuilder.Entity("Accounting.Domain.Models.BetEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Bet")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Premium")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("BetEmployees");
                });

            modelBuilder.Entity("Accounting.Domain.Models.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Accounting.Domain.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Accounting.Domain.Models.NotBetEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Premium")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("NotBetEmployees");
                });

            modelBuilder.Entity("Accounting.Domain.Models.WorkDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Hours")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("WorkDays");
                });

            modelBuilder.Entity("DocumentNotBetEmployee", b =>
                {
                    b.Property<Guid>("DocumentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DocumentsId", "EmployeesId");

                    b.HasIndex("EmployeesId");

                    b.ToTable("DocumentNotBetEmployee");
                });

            modelBuilder.Entity("Accounting.Domain.Models.Accrual", b =>
                {
                    b.HasOne("Accounting.Domain.Models.Document", "Document")
                        .WithMany("Accruals")
                        .HasForeignKey("DocumentId");

                    b.HasOne("Accounting.Domain.Models.NotBetEmployee", "Employee")
                        .WithMany("Accruals")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Accounting.Domain.Models.BetEmployee", b =>
                {
                    b.HasOne("Accounting.Domain.Models.Group", "Group")
                        .WithMany("BetEmployees")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Accounting.Domain.Models.NotBetEmployee", b =>
                {
                    b.HasOne("Accounting.Domain.Models.Group", "Group")
                        .WithMany("NotBetEmployees")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Accounting.Domain.Models.WorkDay", b =>
                {
                    b.HasOne("Accounting.Domain.Models.BetEmployee", "Employee")
                        .WithMany("WorkDays")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("DocumentNotBetEmployee", b =>
                {
                    b.HasOne("Accounting.Domain.Models.Document", null)
                        .WithMany()
                        .HasForeignKey("DocumentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accounting.Domain.Models.NotBetEmployee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Accounting.Domain.Models.BetEmployee", b =>
                {
                    b.Navigation("WorkDays");
                });

            modelBuilder.Entity("Accounting.Domain.Models.Document", b =>
                {
                    b.Navigation("Accruals");
                });

            modelBuilder.Entity("Accounting.Domain.Models.Group", b =>
                {
                    b.Navigation("BetEmployees");

                    b.Navigation("NotBetEmployees");
                });

            modelBuilder.Entity("Accounting.Domain.Models.NotBetEmployee", b =>
                {
                    b.Navigation("Accruals");
                });
#pragma warning restore 612, 618
        }
    }
}
