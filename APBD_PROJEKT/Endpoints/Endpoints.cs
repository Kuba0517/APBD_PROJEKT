using APBD_PROJEKT.DTOs;
using APBD_PROJEKT.Exceptions;
using APBD_PROJEKT.Helpers.Enums;
using APBD_PROJEKT.RequestModels;
using APBD_PROJEKT.Services;
using APBD_PROJEKT.Services.AuthService;
using APBD_PROJEKT.Services.ContractService;
using APBD_PROJEKT.Services.IncomeService;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
namespace APBD_PROJEKT.Endpoints;

public static class Endpoints
{
    public static void AddEndpoints(this WebApplication webApplication)
    {
        webApplication.MapGet("/api/clients/{clientId:int}", 
            [Authorize(Roles = nameof(UserType.Employee) + "," + nameof(UserType.Admin))] async (int clientId, IClientService clientService) =>
        {
            try
            {
                return Results.Ok(await clientService.GetClient(clientId));
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });

        webApplication.MapDelete("/api/clients/{clientId:int}", 
            [Authorize(Roles = nameof(UserType.Admin))] async (int clientId, IClientService clientService) =>
        {
            try
            {
                await clientService.DeleteClient(clientId);
                return Results.NoContent();
            }
            catch (ClientNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            catch (CompanyNotDeletableException e)
            {
                return Results.BadRequest();
            }
        });

        webApplication.MapPatch("/api/clients/{clientId:int}",
            [Authorize(Roles = "Admin")] async (int clientId, ClientUpdateDto clientUpdateDto, IClientService clientService,
                IValidator<ClientUpdateDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(clientUpdateDto);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                
                try
                {
                    var result = await clientService.UpdateClient(clientId, clientUpdateDto);
                    return result == null ? Results.StatusCode(304) : Results.Ok(result);
                }
                catch (ClientNotFoundException e)
                {
                    return Results.BadRequest(e.Message);
                }
            });

        webApplication.MapPost("/api/clients",
            [Authorize(Roles = nameof(UserType.Employee) + "," + nameof(UserType.Admin))] async (ClientRequestModel clientRequestModel, IClientService clientService,
                IValidator<ClientRequestModel> validator) =>
            {
                var validationResult = await validator.ValidateAsync(clientRequestModel);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                
                
                var result = await clientService.AddClient(clientRequestModel);

                if (result == null)
                {
                    return Results.BadRequest();
                }

                return Results.Created($"/api/clients/{result.ClientId}", result);
            }
        );
        
        webApplication.MapGet("/api/contracts/{contractId:int}",
            [Authorize(Roles = nameof(UserType.Employee) + "," + nameof(UserType.Admin))] async (int contractId, IContractService contractService) =>
            {
                try
                {
                    return Results.Ok(await contractService.GetContract(contractId));
                }
                catch (NotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
            });

        webApplication.MapPost("/api/contracts",
            [Authorize(Roles = nameof(UserType.Employee) + "," + nameof(UserType.Admin))] async (ContractRequestModel contractRequestModel, IContractService contractService,
                IValidator<ContractRequestModel> validator) =>
            {
                
                var validationResult = await validator.ValidateAsync(contractRequestModel);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                try
                {
                    var result = await contractService.CreateContract(contractRequestModel);
                    return Results.Created($"/api/contracts/{result.ContractId}", result);
                }
                catch (NotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
                catch (InvalidDurationException e)
                {
                    return Results.BadRequest(e.Message);
                }
            });

        webApplication.MapPost("/api/payments",
            [Authorize(Roles = nameof(UserType.Employee) + "," + nameof(UserType.Admin))] async (ContractPaymentRequestModel contractPaymentRequestModel, IContractService contractService, 
                IValidator<ContractPaymentRequestModel> validator) =>
            {
                var validationResult = await validator.ValidateAsync(contractPaymentRequestModel);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                try
                {
                    var result = await contractService.PayForContract(contractPaymentRequestModel);
                    return Results.Created($"/api/payments/{result.ContractPaymentId}", result);
                }
                catch (ContractNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
                catch (DateOutOfBoundException e)
                {
                    return Results.BadRequest(e.Message);
                }
                catch (WrongPaymentException e)
                {
                    return Results.BadRequest(e.Message);
                }
            });

        webApplication.MapGet("/api/current-income",
            [Authorize(Roles = nameof(UserType.Employee) + "," + nameof(UserType.Admin))] async (int? productId, string? currency, IIncomeService incomeService) =>
            {
                try
                {
                    return Results.Ok(await incomeService.CalculateCurrentIncome(productId, currency));
                }
                catch (NoSuchCurrencyException e)
                {
                    return Results.NotFound(e.Message);
                }
            });
        
        webApplication.MapGet("/api/predicted-income",
            [Authorize(Roles = nameof(UserType.Employee))] async (int? productId, string? currency, IIncomeService incomeService) =>
            {
                try
                {
                    return Results.Ok(await incomeService.CalculatePredictedIncome(productId, currency));
                }
                catch (NoSuchCurrencyException e)
                {
                    return Results.NotFound(e.Message);
                }
            });

        webApplication.MapPost("/api/auth/login",
            async (LoginRequestModel loginRequestModel, IAuthService authService,
                IValidator<LoginRequestModel> validator) =>
            {
                var validationResult = await validator.ValidateAsync(loginRequestModel);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                
                
                var response = await authService.Login(loginRequestModel);
                return response != null ? Results.Ok(response) : Results.Unauthorized();
            });

        webApplication.MapPost("/api/auth/refresh",
            async (RefreshTokenRequestModel refreshTokenRequestModel, IAuthService authService,
                IValidator<RefreshTokenRequestModel> validator) =>
            {
                var validationResult = await validator.ValidateAsync(refreshTokenRequestModel);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                
                var response = await authService.RefreshToken(refreshTokenRequestModel.RefreshToken);
                if (response != null)
                {
                    return Results.Ok(response);
                }

                return Results.Unauthorized();
            });
        
        webApplication.MapPost("/api/auth/register",
            async (RegisterRequestModel registerRequestModel, IAuthService authService, 
                IValidator<RegisterRequestModel> validator) =>
            {
                var validationResult = await validator.ValidateAsync(registerRequestModel);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                
                var response = await authService.Register(registerRequestModel);
                if (response)
                {
                    return Results.Ok("User registered successfully");
                }

                return Results.BadRequest("Username aready exists");
            });
    }
}