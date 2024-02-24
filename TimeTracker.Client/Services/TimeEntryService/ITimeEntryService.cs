using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services.TimeEntryService
{
    public interface ITimeEntryService
    {
        event Action? OnChange;
        public List<TimeEntryResponse> TimeEntries { get; set; }
        Task GetTimeEntriesByProjectId(int projectId);
        Task<TimeEntryResponse> GetTimeEntryById(int id);
        Task CreateTimeEntry(TimeEntryRequest request);
        Task UpdateTimeEntry(int id, TimeEntryRequest request);
        Task DeleteTimeEntry(int id);
    }
}
