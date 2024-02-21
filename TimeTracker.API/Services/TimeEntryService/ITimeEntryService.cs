namespace TimeTracker.API.Services.TimeEntryService
{
    public interface ITimeEntryService
    {
        Task<List<TimeEntryResponse>> GetAllTimeEntries();
        Task<TimeEntryResponse?> GetTimeEntryById(int id);
        Task<List<TimeEntryResponse>> GetAllTimeEntriesByProjectId(int projectId);
        Task<List<TimeEntryResponse>> CreateTimeEntry(TimeEntryCreateRequest timeEntry);
        Task<List<TimeEntryResponse>?> UpdateTimeEntry(int id, TimeEntryUpdateRequest timeEntry);
        Task<List<TimeEntryResponse>?> DeleteTimeEntry(int id);
    }
}
