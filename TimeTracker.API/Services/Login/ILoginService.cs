namespace TimeTracker.API.Services.Login
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
}
