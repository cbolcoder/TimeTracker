using System.Net.Http.Json;
using TimeTracker.Shared.Models.Account;

namespace TimeTracker.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task Register(AccountRegistrationRequest request)
        {
            await _http.PostAsJsonAsync("api/account", request);
        }
    }
}
