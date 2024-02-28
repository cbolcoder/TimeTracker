namespace TimeTracker.API.Repositories.TimeEntryRepository
{
    public interface ITimeEntryRepository
    {
        Task<List<TimeEntry>> GetAllTimeEntries();
        Task<TimeEntry?> GetTimeEntryById(int id);
        Task<List<TimeEntry>> GetTimeEntries(int skip, int limit);
        Task<int> GetTimeEntriesCount();
        Task<List<TimeEntry>?> GetTimeEntriesByProjectId(int projectId);
        Task<List<TimeEntry>> CreateTimeEntry(TimeEntry timeEntry);
        Task<List<TimeEntry>> UpdateTimeEntry(int id, TimeEntry timeEntry);
        Task<List<TimeEntry>?> DeleteTimeEntry(int id);
    }
}
