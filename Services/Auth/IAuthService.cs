﻿using Models;
using Models.DTOs.Input;

namespace Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string username, string password);
    }
}