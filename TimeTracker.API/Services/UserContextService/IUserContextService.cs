namespace TimeTracker.API.Services.UserContextService
{
    public interface IUserContextService
    {
        string? GetUserId();
        Task<User?> GetUserAsync();
    }
}
