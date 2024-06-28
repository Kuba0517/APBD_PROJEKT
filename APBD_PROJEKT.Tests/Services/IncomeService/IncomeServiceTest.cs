using System;
using System.Threading.Tasks;
using APBD_PROJEKT.Contexts;
using APBD_PROJEKT.Helpers.CurrencyHelpers;
using APBD_PROJEKT.Helpers.Enums;
using APBD_PROJEKT.Models;
using APBD_PROJEKT.Services.IncomeService;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace APBD_PROJEKT.Tests.Services.IncomeService;

[TestSubject(typeof(APBD_PROJEKT.Services.IncomeService.IncomeService))]
public class IncomeServiceTest
{

    private readonly DbContextOptions<DatabaseContext> _dbContextOptions;

    public IncomeServiceTest()
    {
        _dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }
    
    
    private DatabaseContext CreateContext() => new DatabaseContext(_dbContextOptions);

    [Fact]
    public async Task CalculateCurrentIncome_Returns_Total_Income_In_PLN_When_Product_Id_Was_Not_Provided()
    {
        await using var context = CreateContext();
        
        await SeedData(context);

        var service = new APBD_PROJEKT.Services.IncomeService.IncomeService(context);

        var result = await service.CalculateCurrentIncome(null, null);

        Assert.Equal(1000, result.Income);
        Assert.Equal("PLN", result.Currency);
    }

    [Fact]
    public async Task CalculateCurrentIncome_WithProductId_ReturnsFilteredIncomeInPLN()
    {
        await using var context = CreateContext();
        
        await SeedData(context);

        var service = new APBD_PROJEKT.Services.IncomeService.IncomeService(context);

        var result = await service.CalculateCurrentIncome(1, null);

        Assert.Equal(1000, result.Income);
        Assert.Equal("PLN", result.Currency);
    }

    [Fact]
    public async Task CalculateCurrentIncome_Returns_Income_In_Specified_Currency_When_Currency_Provided()
    {
        await using var context = CreateContext();
        
        await SeedData(context);

        var service = new APBD_PROJEKT.Services.IncomeService.IncomeService(context);

        var currency = "USD";
        var exchangeRate = Currency.GetCurrencyRate(currency);
        var result = await service.CalculateCurrentIncome(null, currency);

        Assert.Equal(1000 * exchangeRate, result.Income);
        Assert.Equal("USD", result.Currency);
    }

    [Fact]
    public async Task CalculatePredictedIncome_Returns_Total_Predicted_Income_In_PLN_When_Product_Id_Not_Provided()
    {
        await using var context = CreateContext();
        
        await SeedData(context);
        
        await context.SaveChangesAsync();

        var service = new APBD_PROJEKT.Services.IncomeService.IncomeService(context);

        var result = await service.CalculatePredictedIncome(null, null);

        Assert.Equal(1000, result.Income);
        Assert.Equal("PLN", result.Currency);
    }

    [Fact]
    public async Task CalculatePredictedIncome_With_Product_Id_Returns_Filtered_Predicted_Income_In_PLN()
    {
        await using var context = CreateContext();

        await SeedData(context);

        var service = new APBD_PROJEKT.Services.IncomeService.IncomeService(context);

        var result = await service.CalculatePredictedIncome(1, null);

        Assert.Equal(1000, result.Income);
        Assert.Equal("PLN", result.Currency);
    }

    private async Task SeedData(DatabaseContext context)
    {
        var mockClient = new IndividualClient()
        {
            ClientId = 1,
            Address = "zlota 32, Warszawa",
            Email = "j@gmail.com",
            PhoneNumber = "+48111222333",
            Name = "Jakub",
            Surname = "Nowak",
            Pesel = "12345678901"
        };

        var mockSoftware = new Software()
        {
            SoftwareId = 1,
            Name = "Software1",
            Description = "Test software",
            CurrentVersion = "1.0",
            SoftwareType = SoftwareType.Education
        };

        var mockContract = new Contract
        {
            ContractId = 1,
            ClientId = 1,
            SoftwareId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(28),
            Price = 1000,
            SupportYears = 1,
            SoftwareVersion = "1.0",
            IsSigned = true
        };

        await context.Clients.AddAsync(mockClient);
        await context.Softwares.AddAsync(mockSoftware);
        await context.Contracts.AddAsync(mockContract);
        await context.SaveChangesAsync();
    }
}