using TimeTracker.Shared.Models.Account;

namespace TimeTracker.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task Register(AccountRegistrationRequest request);
    }
}
