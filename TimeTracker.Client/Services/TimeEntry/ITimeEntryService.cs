using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services.TimeEntry
{
    public interface ITimeEntryService
    {
        event Action? OnChange;
        public List<TimeEntryResponse> TimeEntries { get; set; }
        Task GetTimeEntriesByProjectId(int projectId);
        Task<TimeEntryResponse> GetTimeEntryById(int id);
    }
}
