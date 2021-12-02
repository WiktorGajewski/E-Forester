﻿using E_Forester.Model.Database;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Authenticate(string login, string password);
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string login);
        Task RegisterUserAsync(User newUser);
    }
}