using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, IToastService toastService, NavigationManager navigationManager, ILocalStorageService localStorageService, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _toastService = toastService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _authStateProvider = authStateProvider;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var result = await _http.PostAsJsonAsync("api/login", request);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (response.IsSuccessful)
                {                    
                    if(response.Token != null)
                    {
                        await _localStorageService.SetItemAsStringAsync("authToken", response.Token);
                        await _authStateProvider.GetAuthenticationStateAsync();
                    }
                    _navigationManager.NavigateTo("timeentries");

                }
                return response;
            }
            return new LoginResponse(false);
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            await _authStateProvider.GetAuthenticationStateAsync();
            _navigationManager.NavigateTo("/login");
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
