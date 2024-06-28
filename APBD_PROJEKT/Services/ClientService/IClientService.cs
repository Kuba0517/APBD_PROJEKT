
using APBD_PROJEKT.DTOs;
using APBD_PROJEKT.Models;
using APBD_PROJEKT.RequestModels;
using APBD_PROJEKT.ResponseModels;

namespace APBD_PROJEKT.Services;

public interface IClientService
{
    Task<ClientResponseModel> GetClient(int id);
    Task<bool> DeleteClient(int id);
    Task<ClientResponseModel> AddClient(ClientRequestModel clientRequestModel);

    Task<ClientResponseModel?> UpdateClient(int id, ClientUpdateDto clientUpdateDto);

    // Task<> editClient()
}