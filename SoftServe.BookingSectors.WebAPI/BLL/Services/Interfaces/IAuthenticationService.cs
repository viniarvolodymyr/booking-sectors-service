﻿using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces
{
    /// <summary>
    /// Behavior of authentication
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Checks credential
        /// </summary>
        /// <returns>Access and refresh tokens</returns>
        Task<TokenDTO> SignInAsync(string login, string password);
        /// <summary>
        /// Renews a token
        /// </summary>
        /// <param name="token">Old tokens</param>
        /// <returns>New tokens</returns>
        Task<TokenDTO> TokenAsync(TokenDTO token);

        bool IsPasswordTheSame(User user, string password);
    }
}