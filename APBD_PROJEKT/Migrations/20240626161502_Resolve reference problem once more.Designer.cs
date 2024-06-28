﻿// <auto-generated />
using System;
using APBD_PROJEKT.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APBD_PROJEKT.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240626161502_Resolve reference problem once more")]
    partial class Resolvereferenceproblemoncemore
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.5.24306.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APBD_PROJEKT.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)")
                        .HasColumnName("client_type");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone_number");

                    b.HasKey("ClientId");

                    b.ToTable("clients");

                    b.HasDiscriminator<string>("ClientType").HasValue("Client");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("APBD_PROJEKT.Models.Contract", b =>
                {
                    b.Property<int>("ContractId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContractId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("client_id");

                    b.Property<int?>("DiscountId")
                        .HasColumnType("int")
                        .HasColumnName("discount_id");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit")
                        .HasColumnName("is_paid");

                    b.Property<bool>("IsSigned")
                        .HasColumnType("bit")
                        .HasColumnName("is_signed");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<int>("SoftwareId")
                        .HasColumnType("int")
                        .HasColumnName("software_id");

                    b.Property<string>("SoftwareVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("software_version");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.Property<int>("SupportYears")
                        .HasColumnType("int")
                        .HasColumnName("support_years");

                    b.HasKey("ContractId");

                    b.HasIndex("ClientId");

                    b.HasIndex("DiscountId");

                    b.HasIndex("SoftwareId");

                    b.ToTable("contracts");
                });

            modelBuilder.Entity("APBD_PROJEKT.Models.Discount", b =>
                {
                    b.Property<int>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiscountId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnName("type");

                    b.Property<double>("Value")
                        .HasColumnType("float")
                        .HasColumnName("value");

                    b.HasKey("DiscountId");

                    b.ToTable("discounts");

                    b.HasData(
                        new
                        {
                            DiscountId = 1,
                            EndTime = new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "New Year Discount",
                            StartDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 1,
                            Value = 20.0
                        },
                        new
                        {
                            DiscountId = 2,
                            EndTime = new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Subscription Discount",
                            StartDate = new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 0,
                            Value = 15.0
                        });
                });

            modelBuilder.Entity("APBD_PROJEKT.Models.Software", b =>
                {
                    b.Property<int>("SoftwareId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoftwareId"));

                    b.Property<string>("CurrentVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("current_version");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int>("SoftwareType")
                        .HasColumnType("int")
                        .HasColumnName("software_type");

                    b.HasKey("SoftwareId");

                    b.ToTable("softwares");

                    b.HasData(
                        new
                        {
                            SoftwareId = 1,
                            CurrentVersion = "1.0.0",
                            Description = "Software for managing finances",
                            Name = "Finance Manager",
                            SoftwareType = 0
                        },
                        new
                        {
                            SoftwareId = 2,
                            CurrentVersion = "2.3.1",
                            Description = "Educational software for students",
                            Name = "EduLearn",
                            SoftwareType = 1
                        });
                });

            modelBuilder.Entity("APBD_PROJEKT.Models.Company", b =>
                {
                    b.HasBaseType("APBD_PROJEKT.Models.Client");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("company_name");

                    b.Property<string>("Krs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("krs");

                    b.ToTable("clients");

                    b.HasDiscriminator().HasValue("Company");

                    b.HasData(
                        new
                        {
                            ClientId = 6,
                            Address = "Company Address 1",
                            ClientType = "Company",
                            Email = "company1@example.com",
                            PhoneNumber = "123456789",
                            CompanyName = "Company 1",
                            Krs = "1234567890"
                        },
                        new
                        {
                            ClientId = 7,
                            Address = "Company Address 2",
                            ClientType = "Company",
                            Email = "company2@example.com",
                            PhoneNumber = "123456780",
                            CompanyName = "Company 2",
                            Krs = "1234567891"
                        },
                        new
                        {
                            ClientId = 8,
                            Address = "Company Address 3",
                            ClientType = "Company",
                            Email = "company3@example.com",
                            PhoneNumber = "123456781",
                            CompanyName = "Company 3",
                            Krs = "1234567892"
                        },
                        new
                        {
                            ClientId = 9,
                            Address = "Company Address 4",
                            ClientType = "Company",
                            Email = "company4@example.com",
                            PhoneNumber = "123456782",
                            CompanyName = "Company 4",
                            Krs = "1234567893"
                        },
                        new
                        {
                            ClientId = 10,
                            Address = "Company Address 5",
                            ClientType = "Company",
                            Email = "company5@example.com",
                            PhoneNumber = "123456783",
                            CompanyName = "Company 5",
                            Krs = "1234567894"
                        });
                });

            modelBuilder.Entity("APBD_PROJEKT.Models.IndividualClient", b =>
                {
                    b.HasBaseType("APBD_PROJEKT.Models.Client");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("pesel");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("surname");

                    b.ToTable("clients");

                    b.HasDiscriminator().HasValue("Individual");

                    b.HasData(
                        new
                        {
                            ClientId = 1,
                            Address = "Address 1",
                            ClientType = "Individual",
                            Email = "individual1@example.com",
                            PhoneNumber = "123456789",
                            IsDeleted = false,
                            Name = "John",
                            Pesel = "12345678901",
                            Surname = "Doe"
                        },
                        new
                        {
                            ClientId = 2,
                            Address = "Address 2",
                            ClientType = "Individual",
                            Email = "individual2@example.com",
                            PhoneNumber = "123456780",
                            IsDeleted = false,
                            Name = "Jane",
                            Pesel = "12345678902",
                            Surname = "Doe"
                        },
                        new
                        {
                            ClientId = 3,
                            Address = "Address 3",
                            ClientType = "Individual",
                            Email = "individual3@example.com",
                            PhoneNumber = "123456781",
                            IsDeleted = false,
                            Name = "Alice",
                            Pesel = "12345678903",
                            Surname = "Smith"
                        },
                        new
                        {
                            ClientId = 4,
                            Address = "Address 4",
                            ClientType = "Individual",
                            Email = "individual4@example.com",
                            PhoneNumber = "123456782",
                            IsDeleted = false,
                            Name = "Bob",
                            Pesel = "12345678904",
                            Surname = "Brown"
                        },
                        new
                        {
                            ClientId = 5,
                            Address = "Address 5",
                            ClientType = "Individual",
                            Email = "individual5@example.com",
                            PhoneNumber = "123456783",
                            IsDeleted = false,
                            Name = "Charlie",
                            Pesel = "12345678905",
                            Surname = "Davis"
                        });
                });

            modelBuilder.Entity("APBD_PROJEKT.Models.Contract", b =>
                {
                    b.HasOne("APBD_PROJEKT.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APBD_PROJEKT.Models.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId");

                    b.HasOne("APBD_PROJEKT.Models.Software", "Software")
                        .WithMany()
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Discount");

                    b.Navigation("Software");
                });
#pragma warning restore 612, 618
        }
    }
}
