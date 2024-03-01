using TimeTracker.Shared.Models.Account;

namespace TimeTracker.API.Services.AccountService
{
    public interface IAccountService
    {
        Task<AccountRegistrationResponse> RegisterAsync(AccountRegistrationRequest request);
    }
}
