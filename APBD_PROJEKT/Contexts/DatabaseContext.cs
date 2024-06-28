using APBD_PROJEKT.Helpers.Enums;
using APBD_PROJEKT.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_PROJEKT.Contexts;

public class DatabaseContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<ContractPayment> ContractPayments { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasDiscriminator<ClientType>("ClientType")
            .HasValue<IndividualClient>(ClientType.Individual)
            .HasValue<Company>(ClientType.Company);


        modelBuilder.Entity<IndividualClient>().HasData(
            new IndividualClient
            {
                ClientId = 1,
                Address = "Address 1",
                Email = "individual1@example.com",
                PhoneNumber = "123456789",
                Name = "John",
                Surname = "Doe",
                Pesel = "12345678901",
                IsDeleted = false
            },
            new IndividualClient
            {
                ClientId = 2,
                Address = "Address 2",
                Email = "individual2@example.com",
                PhoneNumber = "123456780",
                Name = "Jane",
                Surname = "Doe",
                Pesel = "12345678902",
                IsDeleted = false
            },
            new IndividualClient
            {
                ClientId = 3,
                Address = "Address 3",
                Email = "individual3@example.com",
                PhoneNumber = "123456781",
                Name = "Alice",
                Surname = "Smith",
                Pesel = "12345678903",
                IsDeleted = false
            },
            new IndividualClient
            {
                ClientId = 4,
                Address = "Address 4",
                Email = "individual4@example.com",
                PhoneNumber = "123456782",
                Name = "Bob",
                Surname = "Brown",
                Pesel = "12345678904",
                IsDeleted = false
            },
            new IndividualClient
            {
                ClientId = 5,
                Address = "Address 5",
                Email = "individual5@example.com",
                PhoneNumber = "123456783",
                Name = "Charlie",
                Surname = "Davis",
                Pesel = "12345678905",
                IsDeleted = false
            }
        );

        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                ClientId = 6,
                Address = "Company Address 1",
                Email = "company1@example.com",
                PhoneNumber = "123456789",
                CompanyName = "Company 1",
                Krs = "1234567890"
            },
            new Company
            {
                ClientId = 7,
                Address = "Company Address 2",
                Email = "company2@example.com",
                PhoneNumber = "123456780",
                CompanyName = "Company 2",
                Krs = "1234567891"
            },
            new Company
            {
                ClientId = 8,
                Address = "Company Address 3",
                Email = "company3@example.com",
                PhoneNumber = "123456781",
                CompanyName = "Company 3",
                Krs = "1234567892"
            },
            new Company
            {
                ClientId = 9,
                Address = "Company Address 4",
                Email = "company4@example.com",
                PhoneNumber = "123456782",
                CompanyName = "Company 4",
                Krs = "1234567893"
            },
            new Company
            {
                ClientId = 10,
                Address = "Company Address 5",
                Email = "company5@example.com",
                PhoneNumber = "123456783",
                CompanyName = "Company 5",
                Krs = "1234567894"
            }
        );
        
        modelBuilder.Entity<Software>().HasData(
            new Software
            {
                SoftwareId = 1,
                Name = "Finance Manager",
                Description = "Software for managing finances",
                CurrentVersion = "1.0.0",
                SoftwareType = SoftwareType.Finances
            },
            new Software
            {
                SoftwareId = 2,
                Name = "EduLearn",
                Description = "Educational software for students",
                CurrentVersion = "2.3.1",
                SoftwareType = SoftwareType.Education
            }
        );
        
        modelBuilder.Entity<Discount>().HasData(
            new Discount
            {
                DiscountId = 1,
                Name = "New Year Discount",
                Type = DiscountType.Normal,
                Value = 20,
                StartDate = new DateTime(2024, 1, 1),
                EndTime = new DateTime(2024, 1, 31)
            },
            new Discount
            {
                DiscountId = 2,
                Name = "Subscription Discount",
                Type = DiscountType.Subscription,
                Value = 15,
                StartDate = new DateTime(2024, 2, 1),
                EndTime = new DateTime(2024, 2, 28)
            }
        );
    }
}