using System;
using System.Threading.Tasks;
using APBD_PROJEKT.Contexts;
using APBD_PROJEKT.Exceptions;
using APBD_PROJEKT.Helpers.Enums;
using APBD_PROJEKT.Models;
using APBD_PROJEKT.RequestModels;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace APBD_PROJEKT.Tests.Services.ContractService;

[TestSubject(typeof(APBD_PROJEKT.Services.ContractService.ContractService))]
public class ContractServiceTest
{
    private readonly DbContextOptions<DatabaseContext> _dbContextOptions;

    public ContractServiceTest()
    {
        _dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }
    
    private DatabaseContext CreateContext() => new DatabaseContext(_dbContextOptions);
    
    //Arrange
    //Act
    //Assert

    [Fact]
    public async Task CreateContract_Invalid_Duration_Throws_InvalidDurationException()
    {
        await using var context = CreateContext();
        var service = new APBD_PROJEKT.Services.ContractService.ContractService(context);

        var contractRequestModel = new ContractRequestModel
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(2),
            ClientId = 1,
            SoftwareId = 1,
            Price = 1000,
            SupportYears = 1,
            SoftwareVersion = "1.0",
            IsSigned = false
        };

        await Assert.ThrowsAsync<InvalidDurationException>(() =>
            service.CreateContract(contractRequestModel));
    }

    [Fact]
    public async Task CreateContract_Throws_ClientNotFoundException_When_Provided_Wrong_Client_Id()
    {
        await using var context = CreateContext();
        var service = new APBD_PROJEKT.Services.ContractService.ContractService(context);

        var contractRequestModel = new ContractRequestModel
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(5),
            ClientId = 99,
            SoftwareId = 1,
            Price = 1000,
            SupportYears = 1,
            SoftwareVersion = "1.0",
            IsSigned = false
        };

        await Assert.ThrowsAsync<ClientNotFoundException>(() =>
            service.CreateContract(contractRequestModel));
    }

    [Fact]
    public async Task CreateContract_Throws_SoftwareNotFoundException_When_Provided_Wrong_Software_Id()
    {
        await using var context = CreateContext();
        var service = new APBD_PROJEKT.Services.ContractService.ContractService(context);
        
        var mockIndividualClient = new IndividualClient()
        {
            ClientId = 1,
            Address = "zlota 32, Warszawa",
            Email = "j@gmail.com",
            PhoneNumber = "+48111222333",
            Name = "Jakub",
            Surname = "Nowak",
            Pesel = "12345678901"
        };

        await context.Clients.AddAsync(mockIndividualClient);
        await context.SaveChangesAsync();

        var contractRequestModel = new ContractRequestModel
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(5),
            ClientId = 1,
            SoftwareId = 999,
            Price = 1000,
            SupportYears = 1,
            SoftwareVersion = "1.0",
            IsSigned = false
        };

        await Assert.ThrowsAsync<SoftwareNotFoundException>(() =>
            service.CreateContract(contractRequestModel));
    }

    [Fact]
    public async Task GetContract_Throws_ContractNotFoundException_When_Provided_Wrong_Contract_Id()
    {
        await using var context = CreateContext();
        var service = new APBD_PROJEKT.Services.ContractService.ContractService(context);

        var invalidContractId = 999;

        await Assert.ThrowsAsync<ContractNotFoundException>(() =>
            service.GetContract(invalidContractId));
    }

    [Fact]
    public async Task PayForContract_ThrowsWrongPaymentException_When_Provided_Wrong_Payment()
    {
        await using var context = CreateContext();
        var service = new APBD_PROJEKT.Services.ContractService.ContractService(context);
        
        var contract = new Contract
        {
            ContractId = 1,
            ClientId = 1,
            SoftwareId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(10),
            Price = 1000,
            SupportYears = 1,
            SoftwareVersion = "1.0",
            IsSigned = false
        };
        await context.Contracts.AddAsync(contract);
        await context.SaveChangesAsync();

        var contractPaymentRequestModel = new ContractPaymentRequestModel
        {
            ContractId = 1,
            PaymentType = PaymentType.Single,
            Value = 500
        };

        await Assert.ThrowsAsync<WrongPaymentException>(() =>
            service.PayForContract(contractPaymentRequestModel));
    }
}
