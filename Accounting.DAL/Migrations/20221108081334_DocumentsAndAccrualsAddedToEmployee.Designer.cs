﻿// <auto-generated />
using System;
using Accounting.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Accounting.DAL.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20221108081334_DocumentsAndAccrualsAddedToEmployee")]
    partial class DocumentsAndAccrualsAddedToEmployee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Accounting.Domain.Models.PayoutBetEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Ammount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("BetEmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAdditional")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BetEmployeeId");

                    b.HasIndex("DocumentId");

                    b.ToTable("AccrualsBetEmployee");
                });

            modelBuilder.Entity("Accounting.Domain.Models.PayoutNotBetEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Ammount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAdditional")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AccrualsNotBetEmployee");
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

            modelBuilder.Entity("Accounting.Domain.Models.DeducationBetEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Ammount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAdditional")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("DeducationBetEmployees");
                });

            modelBuilder.Entity("Accounting.Domain.Models.DeducationDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DeducationDocuments");
                });

            modelBuilder.Entity("Accounting.Domain.Models.DeducationNotBetEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Ammount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAdditional")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("DeducationNotBetEmployees");
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

            modelBuilder.Entity("BetEmployeeDeducationDocument", b =>
                {
                    b.Property<Guid>("BetEmployeesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeducationDocumentsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BetEmployeesId", "DeducationDocumentsId");

                    b.HasIndex("DeducationDocumentsId");

                    b.ToTable("BetEmployeeDeducationDocument");
                });

            modelBuilder.Entity("BetEmployeeDocument", b =>
                {
                    b.Property<Guid>("BetEmployeesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DocumentsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BetEmployeesId", "DocumentsId");

                    b.HasIndex("DocumentsId");

                    b.ToTable("BetEmployeeDocument");
                });

            modelBuilder.Entity("DeducationDocumentNotBetEmployee", b =>
                {
                    b.Property<Guid>("DeducationDocumentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NotBetEmployeesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DeducationDocumentsId", "NotBetEmployeesId");

                    b.HasIndex("NotBetEmployeesId");

                    b.ToTable("DeducationDocumentNotBetEmployee");
                });

            modelBuilder.Entity("DocumentNotBetEmployee", b =>
                {
                    b.Property<Guid>("DocumentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NotBetEmployeesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DocumentsId", "NotBetEmployeesId");

                    b.HasIndex("NotBetEmployeesId");

                    b.ToTable("DocumentNotBetEmployee");
                });

            modelBuilder.Entity("Accounting.Domain.Models.PayoutBetEmployee", b =>
                {
                    b.HasOne("Accounting.Domain.Models.BetEmployee", "BetEmployee")
                        .WithMany("Accruals")
                        .HasForeignKey("BetEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accounting.Domain.Models.Document", "Document")
                        .WithMany("AccrualsBetEmplyee")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BetEmployee");

                    b.Navigation("Document");
                });

            modelBuilder.Entity("Accounting.Domain.Models.PayoutNotBetEmployee", b =>
                {
                    b.HasOne("Accounting.Domain.Models.Document", "Document")
                        .WithMany("AccrualsNotBetEmployee")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("Accounting.Domain.Models.DeducationBetEmployee", b =>
                {
                    b.HasOne("Accounting.Domain.Models.DeducationDocument", "Document")
                        .WithMany("DeducationsBetEmployee")
                        .HasForeignKey("DocumentId");

                    b.HasOne("Accounting.Domain.Models.BetEmployee", "Employee")
                        .WithMany("DeducationBetEmployee")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Accounting.Domain.Models.DeducationNotBetEmployee", b =>
                {
                    b.HasOne("Accounting.Domain.Models.DeducationDocument", "Document")
                        .WithMany("DeducationsNotBetEmployee")
                        .HasForeignKey("DocumentId");

                    b.HasOne("Accounting.Domain.Models.NotBetEmployee", "Employee")
                        .WithMany("DeducationNotBetEmployee")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Employee");
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

            modelBuilder.Entity("BetEmployeeDeducationDocument", b =>
                {
                    b.HasOne("Accounting.Domain.Models.BetEmployee", null)
                        .WithMany()
                        .HasForeignKey("BetEmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accounting.Domain.Models.DeducationDocument", null)
                        .WithMany()
                        .HasForeignKey("DeducationDocumentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BetEmployeeDocument", b =>
                {
                    b.HasOne("Accounting.Domain.Models.BetEmployee", null)
                        .WithMany()
                        .HasForeignKey("BetEmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accounting.Domain.Models.Document", null)
                        .WithMany()
                        .HasForeignKey("DocumentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DeducationDocumentNotBetEmployee", b =>
                {
                    b.HasOne("Accounting.Domain.Models.DeducationDocument", null)
                        .WithMany()
                        .HasForeignKey("DeducationDocumentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accounting.Domain.Models.NotBetEmployee", null)
                        .WithMany()
                        .HasForeignKey("NotBetEmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                        .HasForeignKey("NotBetEmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Accounting.Domain.Models.BetEmployee", b =>
                {
                    b.Navigation("Accruals");

                    b.Navigation("DeducationBetEmployee");

                    b.Navigation("WorkDays");
                });

            modelBuilder.Entity("Accounting.Domain.Models.DeducationDocument", b =>
                {
                    b.Navigation("DeducationsBetEmployee");

                    b.Navigation("DeducationsNotBetEmployee");
                });

            modelBuilder.Entity("Accounting.Domain.Models.Document", b =>
                {
                    b.Navigation("AccrualsBetEmplyee");

                    b.Navigation("AccrualsNotBetEmployee");
                });

            modelBuilder.Entity("Accounting.Domain.Models.Group", b =>
                {
                    b.Navigation("BetEmployees");

                    b.Navigation("NotBetEmployees");
                });

            modelBuilder.Entity("Accounting.Domain.Models.NotBetEmployee", b =>
                {
                    b.Navigation("Accruals");

                    b.Navigation("DeducationNotBetEmployee");
                });
#pragma warning restore 612, 618
        }
    }
}
