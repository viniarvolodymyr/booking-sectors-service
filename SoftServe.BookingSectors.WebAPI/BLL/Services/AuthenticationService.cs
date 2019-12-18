using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SoftServe.BookingSectors.WebApi.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using SoftServe.BookingSectors.WebAPI.DAL.Repositories.ImplementationRepositories;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtFactory _jwtFactory;

        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            IJwtFactory jwtFactory,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _jwtFactory = jwtFactory;
        }

        public async Task<TokenDTO> SignInAsync(string login, string password)
        {
            try
            {
                var user = (await UserRepository.GetAllEntitiesAsync(u => u.Login == Login))
                    .SingleOrDefault();

                if (user != null && (bool)user.IsActive && _hasher.CheckMatch(password, user.Password))
                {
                    var role = await _unitOfWork.RoleRepository.GetByIdAsync((int)user.RoleId);
                    var token = _jwtFactory.GenerateToken(user.Id, user.Login, role?.Name);

                    if (token == null) return null;

                    await _unitOfWork.TokenRepository.AddAsync(new Token
                    {
                        RefreshToken = token.RefreshToken,
                        CreateId = user.Id,
                        Create = user
                    });
                    await _unitOfWork.SaveAsync();
                    return token;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(SignInAsync));
                throw e;
            }
        }

        public async Task<TokenDTO> TokenAsync(TokenDTO token)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(
                    int.Parse(
                        _jwtFactory.GetPrincipalFromExpiredToken(token.AccessToken).jwt.Subject
                        )
                    );
                var newToken = _jwtFactory.GenerateToken(user.Id, user.Login, user.Role.Name);

                await _unitOfWork.TokenRepository.AddAsync(new Token
                {
                    RefreshToken = newToken.RefreshToken,
                    CreateId = user.Id,
                    Create = user
                });
                await _unitOfWork.SaveAsync();
                return newToken;
            }
            catch (Exception e)
                when (e is SecurityTokenException || e is DbUpdateException)
            {
                _logger.LogError(e, nameof(TokenAsync));
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(TokenAsync));
                throw e;
            }
        }
}
