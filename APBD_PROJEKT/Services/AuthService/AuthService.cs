using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using APBD_PROJEKT.Contexts;
using APBD_PROJEKT.Models;
using APBD_PROJEKT.RequestModels;
using APBD_PROJEKT.ResponseModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APBD_PROJEKT.Services.AuthService;

public class AuthService(IConfiguration config, DatabaseContext context) : IAuthService
{
    public async Task<LoginResponseModel> Login(LoginRequestModel model)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
            if (user == null || !VerifyPassword(user.Password, model.Password, user.Salt))
            {
                return null;
            }
            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiryDate = DateTime.Now.AddDays(3),
                UserId = user.UserId
            });
            await context.SaveChangesAsync();

            return new LoginResponseModel
            {
                Token = token,
                RefreshToken = refreshToken
            };

        }

        public async Task<LoginResponseModel> RefreshToken(string refreshToken)
        {
            var storedToken = await context.RefreshTokens.Include(rt => rt.User)
                .SingleOrDefaultAsync(rt => rt.Token == refreshToken);
            if (storedToken == null || storedToken.ExpiryDate <= DateTime.Now)
            {
                return null;
            }
            var user = storedToken.User;
            var newJwtToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            storedToken.Token = newRefreshToken;
            storedToken.ExpiryDate = DateTime.Now.AddDays(3);
            context.RefreshTokens.Update(storedToken);
            await context.SaveChangesAsync();

            return new LoginResponseModel
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken
            };

        }

        public async Task<bool> Register(RegisterRequestModel model)
        {
            if (await context.Users.AnyAsync(u => u.Login == model.Login))
                return false;

            var (hashedPassword, salt) = HashPassword(model.Password);

            var user = new User
            {
                Login = model.Login,
                Password = hashedPassword,
                Salt = salt,
                UserRole = model.UserType
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(config["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.UserRole.ToString())
                }),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = config["JWT:Issuer"],
                Audience = config["JWT:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private static Tuple<string, string> HashPassword(string password)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));

            var saltBase64 = Convert.ToBase64String(salt);

            return new Tuple<string, string>(hashed, saltBase64);
        }

        private static bool VerifyPassword(string storedHash, string password, string storedSalt)
        {
            var salt = Convert.FromBase64String(storedSalt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));

            return hashed == storedHash;
        }
}