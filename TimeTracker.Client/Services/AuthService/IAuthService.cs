﻿using TimeTracker.Shared.Models.Account;
using TimeTracker.Shared.Models.Login;

namespace TimeTracker.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<AccountRegistrationResponse> Register(AccountRegistrationRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task Logout();
    }
}
