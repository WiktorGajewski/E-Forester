﻿using E_Forester.Model.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Authenticate(string login, string password);
        Task<ICollection<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string login);
        Task RegisterUserAsync(User newUser);
        Task AddRefreshToken(RefreshToken token, User user);
        Task<User> GetUserByRefreshTokenAsync(string token);
        Task RevokeRefreshTokenAsync(RefreshToken token);
        Task RemoveExpiredRefreshTokensAsync(User user);
        Task RevokeAllRefreshTokens(User user);
    }
}
