using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using TimeTracker.Shared.Models.Account;
using TimeTracker.Shared.Models.Login;

namespace TimeTracker.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly IToastService _toastService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;

        public AuthService(HttpClient http, IToastService toastService, NavigationManager navigationManager, ILocalStorageService localStorageService)
        {
            _http = http;
            _toastService = toastService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Login(LoginRequest request)
        {
            var result = await _http.PostAsJsonAsync("api/login", request);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (!response.IsSuccessful && response.Error != null)
                {
                    _toastService.ShowError(response.Error);
                }
                else if (!response.IsSuccessful)
                {
                    _toastService.ShowError("An unexpected error occurred.");
                }
                else
                {
                    _toastService.ShowSuccess("Login successful!");
                    _navigationManager.NavigateTo("timeentries");
                    if(response.Token != null)
                    {
                        await _localStorageService.SetItemAsStringAsync("authToken", response.Token);
                    }
                }
            }

        }

        public async Task Register(AccountRegistrationRequest request)
        {
            var result = await _http.PostAsJsonAsync("api/account", request);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<AccountRegistrationResponse>();
                if (!response.IsSuccessful && response.Errors != null)
                {
                    foreach (var error in response.Errors)
                    {
                        _toastService.ShowError(error);
                    }
                }
                else if (!response.IsSuccessful)
                {
                    _toastService.ShowError("An unexpected error occurred.");
                }
                else
                {
                    _toastService.ShowSuccess("Successfully registered! Login to continue.");
                }
            }
        }
    }
}
