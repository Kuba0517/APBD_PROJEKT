using APBD_PROJEKT.RequestModels;
using APBD_PROJEKT.ResponseModels;

namespace APBD_PROJEKT.Services.AuthService;

public interface IAuthService
{
    Task<LoginResponseModel> Login(LoginRequestModel model);
    Task<LoginResponseModel> RefreshToken(string refreshToken);
    Task<bool> Register(RegisterRequestModel model);
}