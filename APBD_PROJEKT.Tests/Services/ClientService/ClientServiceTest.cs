using System;
using System.Threading.Tasks;
using APBD_PROJEKT.Contexts;
using APBD_PROJEKT.DTOs;
using APBD_PROJEKT.Exceptions;
using APBD_PROJEKT.Models;
using APBD_PROJEKT.RequestModels;
using APBD_PROJEKT.ResponseModels;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace APBD_PROJEKT.Tests.Services.ClientService;

[TestSubject(typeof(APBD_PROJEKT.Services.ClientService))]
public class ClientServiceTest
{
    
    private readonly DbContextOptions<DatabaseContext> _dbContextOptions;

    public ClientServiceTest()
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
    public async Task AddClient_Should_Return_IndividualClientResponseModel_When_Added_New_IndividualClient()
    {
        await using var context = CreateContext();
        var clientService = new APBD_PROJEKT.Services.ClientService(context);

        var clientRequestModel = new ClientRequestModel()
        {
            Address = "zlota 32, Warszawa",
            Email = "j@gmail.com",
            PhoneNumber = "+48111222333",
            Name = "Jakub",
            Surname = "Nowak",
            Pesel = "12345678901"
        };

        var result = (IndividualClientResponseModel) await clientService.AddClient(clientRequestModel); 
        
        Assert.Equal(clientRequestModel.Address, result.Address);
        Assert.Equal(clientRequestModel.Email, result.Email);
        Assert.Equal(clientRequestModel.PhoneNumber, result.PhoneNumber);
        Assert.Equal(clientRequestModel.Name, result.Name);
        Assert.Equal(clientRequestModel.Surname, result.Surname);
        Assert.Equal(clientRequestModel.Pesel, result.Pesel);
    }
    
    [Fact]
    public async Task AddClient_Should_Return_CompanyResponseModel_When_Added_New_Company()
    {
        await using var context = CreateContext();
        var clientService = new APBD_PROJEKT.Services.ClientService(context);

        var clientRequestModel = new ClientRequestModel()
        {
            Address = "zlota 32, Warszawa",
            Email = "j@gmail.com",
            PhoneNumber = "+48111222333",
            CompanyName = "APBD corp",
            Krs = "1234567890"
        };

        var result = (CompanyResponseModel) await clientService.AddClient(clientRequestModel); 
        
        Assert.Equal(clientRequestModel.Address, result.Address);
        Assert.Equal(clientRequestModel.Email, result.Email);
        Assert.Equal(clientRequestModel.PhoneNumber, result.PhoneNumber);
        Assert.Equal(clientRequestModel.CompanyName, result.CompanyName);
        Assert.Equal(clientRequestModel.Krs, result.Krs);
    }
    
    [Fact]
    public async Task AddClient_Should_Throw_ClientWrongDataException_When_Provided_Null_Data()
    {
        await using var context = CreateContext();
        var clientService = new APBD_PROJEKT.Services.ClientService(context);

        var clientRequestModel = new ClientRequestModel()
        {
            Address = "zlota 32, Warszawa",
            Email = "j@gmail.com",
            PhoneNumber = "+48111222333",
        };

        await Assert.ThrowsAsync<ClientWrongDataException>(() => clientService.AddClient(clientRequestModel));
    }
    
    [Fact]
    public async Task AddClient_Should_Throw_When_Provided_Null_Data()
    {
        await using var context = CreateContext();
        var clientService = new APBD_PROJEKT.Services.ClientService(context);

        var clientRequestModel = new ClientRequestModel()
        {
            Address = "zlota 32, Warszawa",
            Email = "j@gmail.com",
            PhoneNumber = "+48111222333",
        };

        await Assert.ThrowsAsync<ClientWrongDataException>(() => clientService.AddClient(clientRequestModel));
    }
    
    [Fact]
    public async Task UpdateClient_Should_Throw_ClientNotFoundException_When_Client_Does_Not_Exist()
    {
        await using var context = new DatabaseContext(_dbContextOptions);
        var service = new APBD_PROJEKT.Services.ClientService(context);
        var updateDto = new ClientUpdateDto("New Address", "newemail@example.com", "123456789", null, null, null);
        
        await Assert.ThrowsAsync<ClientNotFoundException>(() => service.UpdateClient(1, updateDto));
    }

    [Fact]
    public async Task UpdateClient_Should_Update_IndividualClient_When_Client_Exists()
    {
        await using var context = CreateContext();
        var client = new IndividualClient
        {
            Address = "Old Address",
            Email = "oldemail@example.com",
            PhoneNumber = "987654321",
            Name = "Old Name",
            Surname = "Old Surname",
            Pesel = "12345678901"
        };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var service = new APBD_PROJEKT.Services.ClientService(context);
        var updateDto = new ClientUpdateDto("New Address", "newemail@example.com", "123456789", null, "New Name", "New Surname");
        
        var result = (IndividualClientResponseModel) await service.UpdateClient(client.ClientId, updateDto);
        
        Assert.Equal(updateDto.Address, result.Address);
        Assert.Equal(updateDto.Email, result.Email);
        Assert.Equal(updateDto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(updateDto.Name, result.Name);
        Assert.Equal(updateDto.Surname, result.Surname);
    }

    [Fact]
    public async Task UpdateClient_ShouldUpdateCompanyClient_WhenClientExists()
    {
        await using var context = CreateContext();
        var client = new Company
        {
            Address = "Old Address",
            Email = "oldemail@example.com",
            PhoneNumber = "987654321",
            CompanyName = "Old Company",
            Krs = "1234567890"
        };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var service = new APBD_PROJEKT.Services.ClientService(context);
        var updateDto = new ClientUpdateDto("New Address", "newemail@example.com", "123456789", "New Company", null, null);
        
        var result = await service.UpdateClient(client.ClientId, updateDto);
        
        Assert.NotNull(result);
        var companyClientResult = Assert.IsType<CompanyResponseModel>(result);
        Assert.Equal("New Address", companyClientResult.Address);
        Assert.Equal("newemail@example.com", companyClientResult.Email);
        Assert.Equal("123456789", companyClientResult.PhoneNumber);
        Assert.Equal("New Company", companyClientResult.CompanyName);
    }

    [Fact]
    public async Task UpdateClient_Should_Return_Null_When_No_Changes_Are_Made()
    {
        await using var context = CreateContext();
        var client = new IndividualClient
        {
            Address = "Address",
            Email = "email@example.com",
            PhoneNumber = "987654321",
            Name = "Name",
            Surname = "Surname",
            Pesel = "12345678901"
        };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var service = new APBD_PROJEKT.Services.ClientService(context);
        var updateDto = new ClientUpdateDto(null, null, null, null, null, null);
        
        var result = await service.UpdateClient(client.ClientId, updateDto);
        
        Assert.Null(result);
    }
    
    [Fact]
    public async Task DeleteClient_Should_Throw_ClientNotFoundException_When_Client_Does_Not_Exist()
    {
        await using var context = CreateContext();
        var service = new APBD_PROJEKT.Services.ClientService(context);
        
        await Assert.ThrowsAsync<ClientNotFoundException>(() => service.DeleteClient(1));
    }

    [Fact]
    public async Task DeleteClient_Should_Mark_IndividualClient_As_Deleted_When_Client_Is_Individual()
    {
        await using var context = CreateContext();
        var client = new IndividualClient
        {
            Address = "Address",
            Email = "email@example.com",
            PhoneNumber = "987654321",
            Name = "Name",
            Surname = "Surname",
            Pesel = "12345678901"
        };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var service = new APBD_PROJEKT.Services.ClientService(context);
        
        var result = await service.DeleteClient(client.ClientId);
        
        Assert.True(result);
        var deletedClient = (IndividualClient) await context.Clients.FindAsync(client.ClientId);
        Assert.NotNull(deletedClient);
        Assert.True(deletedClient.IsDeleted);
    }

    [Fact]
    public async Task DeleteClient_Should_Throw_Company_Not_Deletable_Exception_When_Client_Is_Company()
    {
        await using var context = CreateContext();
        var client = new Company
        {
            Address = "Address",
            Email = "email@example.com",
            PhoneNumber = "987654321",
            CompanyName = "Company",
            Krs = "1234567890"
        };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var service = new APBD_PROJEKT.Services.ClientService(context);
        
        await Assert.ThrowsAsync<CompanyNotDeletableException>(() => service.DeleteClient(client.ClientId));
    }

    [Fact]
    public async Task GetClient_Throws_ClientNotFoundException_When_Provided_Wrong_Client_Id()
    {
        await using var context = CreateContext();
        var service = new APBD_PROJEKT.Services.ClientService(context);
        await Assert.ThrowsAsync<ClientNotFoundException>(() => service.GetClient(999));
    }
}