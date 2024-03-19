using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services.TimeEntryService
{
    public interface ITimeEntryService
    {
        event Action? OnChange;
        public List<TimeEntryResponse> TimeEntries { get; set; }
        public TimeSpan TotalDuration { get; set; }
        Task GetTimeEntriesByProject(int projectId);
        Task<TimeEntryResponse> GetTimeEntryById(int id);
        Task CreateTimeEntry(TimeEntryRequest request);
        Task UpdateTimeEntry(int id, TimeEntryRequest request);
        Task DeleteTimeEntry(int id);
        Task GetTimeEntriesByYear(int year);
        Task GetTimeEntriesByMonth(int month, int year);
        Task GetTimeEntriesByDay(int day, int month, int year);
    }
}
