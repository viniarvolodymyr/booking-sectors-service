using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.Jwt;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;
        private readonly IJwtFactory jwtFactory;

        public AuthenticationService(IUnitOfWork database, IMapper mapper, IJwtFactory jwtFactory)
        {
            this.database = database;
            this.mapper = mapper;
            this.jwtFactory = jwtFactory;
        }



        public async Task<TokenDTO> SignInAsync(string phone, string password)
        {
            var user = await database.UserRepository
                .GetByCondition(u => u.Phone == phone)
                .SingleOrDefaultAsync();

            if (IsPasswordTheSame(user, password) == false)
            {
                return null;
            }

            TokenDTO token = jwtFactory.GenerateToken(user.Id, user.Phone, user.Role.Role);

            if (token == null)
            {
                return null;
            }

            await database.TokenRepository.InsertEntityAsync(
                new Token
                {
                    RefreshToken = token.RefreshToken
                }
            );

            bool IsSave = await database.SaveAsync();
            return IsSave
                ? token
                : null;

        }

        private bool IsPasswordTheSame(User user, string password)
        {
            if (user == null) return false;
            var hashedPassword = SHA256Hash.Compute(password);

            return hashedPassword.Zip(user.Password, (a, b) => a == b).Contains(false) == false;
        }

        public async Task<TokenDTO> TokenAsync(TokenDTO token)
        {
            var user = await database.UserRepository.GetEntityByIdAsync(
                int.Parse(
                    jwtFactory.GetPrincipalFromExpiredToken(token.AccessToken).jwt.Subject
                    )
            );

            var newToken = jwtFactory.GenerateToken(user.Id, user.Phone, user.Role.ToString());

            await database.TokenRepository.InsertEntityAsync(new Token
            {
                RefreshToken = newToken.RefreshToken,
            });
            bool IsSave = await database.SaveAsync();
            return IsSave
                ? newToken
                : null;
        }
    }
}
