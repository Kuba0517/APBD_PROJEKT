
using APBD_PROJEKT.Contexts;
using APBD_PROJEKT.DTOs;
using APBD_PROJEKT.Exceptions;
using APBD_PROJEKT.Models;
using APBD_PROJEKT.RequestModels;
using APBD_PROJEKT.ResponseModels;

namespace APBD_PROJEKT.Services;

public class ClientService(DatabaseContext context) : IClientService
{
    public async Task<ClientResponseModel> GetClient(int id)
    {
        var client = await context.Clients.FindAsync(id);
        ClientResponseModel response = null;

        if (client is Company company)
        {
            response = new CompanyResponseModel()
            {
                Address = company.Address,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
                CompanyName = company.CompanyName,
                Krs = company.Krs
            };
        }
        
        if (client is IndividualClient individualClient)
        {
            response = new IndividualClientResponseModel()
            {
                Address = individualClient.Address,
                Email = individualClient.Email,
                PhoneNumber = individualClient.PhoneNumber,
                Name = individualClient.Name,
                Surname = individualClient.Surname,
                Pesel = individualClient.Pesel
            };
        }

        if (response == null)
        {
            throw new ClientNotFoundException($"Client with id: {id} has not been found");
        }

        return response;

    }

    public async Task<bool> DeleteClient(int id)
    {
        var client = await context.Clients.FindAsync(id);

        if (client == null)
        {
            throw new ClientNotFoundException($"Client with id: ${id} has not been found");
        }

        if (client is IndividualClient individualClient)
        {
            individualClient.IsDeleted = true;
        }
        else
        {
            throw new CompanyNotDeletableException("You are not allowed to delete client which is a company");
        }

        await context.SaveChangesAsync();
        
        return true;

    }

    public async Task<ClientResponseModel> AddClient(ClientRequestModel clientRequestModel)
    {
        if (clientRequestModel.Krs != null && clientRequestModel.CompanyName != null)
        {
            var companyToAdd = new Company()
            {
                Address = clientRequestModel.Address,
                Email = clientRequestModel.Email,
                PhoneNumber = clientRequestModel.PhoneNumber,
                CompanyName = clientRequestModel.CompanyName,
                Krs = clientRequestModel.Krs
            };

            await context.Clients.AddAsync(companyToAdd);
            await context.SaveChangesAsync();

            return new CompanyResponseModel()
            {
                ClientId = companyToAdd.ClientId,
                Address = companyToAdd.Address,
                Email = companyToAdd.Email,
                PhoneNumber = companyToAdd.PhoneNumber,
                CompanyName = companyToAdd.CompanyName,
                Krs = companyToAdd.Krs
            };
        }

        if (clientRequestModel.Name == null || clientRequestModel.Surname == null ||
            clientRequestModel.Pesel == null)
        {
            throw new ClientWrongDataException("Possibly missing data was given");
        }
        
        var individualToAdd = new IndividualClient()
        {
            Address = clientRequestModel.Address,
            Email = clientRequestModel.Email,
            PhoneNumber = clientRequestModel.PhoneNumber,
            Name = clientRequestModel.Name,
            Surname = clientRequestModel.Surname,
            Pesel = clientRequestModel.Pesel
        };
            
        await context.Clients.AddAsync(individualToAdd);
        await context.SaveChangesAsync();

        return new IndividualClientResponseModel()
        {
            ClientId = individualToAdd.ClientId,
            Address = individualToAdd.Address,
            Email = individualToAdd.Email,
            PhoneNumber = individualToAdd.PhoneNumber,
            Name = individualToAdd.Name,
            Surname = individualToAdd.Surname,
            Pesel = individualToAdd.Pesel
        };
    }

    public async Task<ClientResponseModel?> UpdateClient(int id, ClientUpdateDto clientUpdateDto)
    {
        var client = await context.Clients.FindAsync(id);
        var changed = false;

        if (client == null)
        {
            throw new ClientNotFoundException($"Client with id: {id} has not been found");
        }

        if (!string.IsNullOrEmpty(clientUpdateDto.Address))
        {
            client.Address = clientUpdateDto.Address;
            changed = true;
        }

        if (!string.IsNullOrEmpty(clientUpdateDto.Email))
        {
            client.Email = clientUpdateDto.Email;
            changed = true;
        }

        if (!string.IsNullOrEmpty(clientUpdateDto.PhoneNumber))
        {
            client.PhoneNumber = clientUpdateDto.PhoneNumber;
            changed = true;
        }

        switch (client)
        {
            case IndividualClient individualClient:
            {
                if (!string.IsNullOrEmpty(clientUpdateDto.Name))
                {
                    individualClient.Name = clientUpdateDto.Name;
                    changed = true;
                }

                if (!string.IsNullOrEmpty(clientUpdateDto.Surname))
                {
                    individualClient.Surname = clientUpdateDto.Surname;
                    changed = true;
                }

                await context.SaveChangesAsync();
                return changed ? new IndividualClientResponseModel()
                {
                    Address = individualClient.Address,
                    Email = individualClient.Email,
                    PhoneNumber = individualClient.PhoneNumber,
                    Name = individualClient.Name,
                    Surname = individualClient.Surname,
                    Pesel = individualClient.Pesel
                } : null;
            }
            case Company company:
            {
                if (!string.IsNullOrEmpty(clientUpdateDto.CompanyName))
                {
                    company.CompanyName = clientUpdateDto.CompanyName;
                    changed = true;
                }

                await context.SaveChangesAsync();

                return changed ? new CompanyResponseModel()
                {
                    Address = company.Address,
                    Email = company.Email,
                    PhoneNumber = company.PhoneNumber,
                    CompanyName = company.CompanyName,
                    Krs = company.Krs
                } : null;
            }
        }

        return null;
    }
}