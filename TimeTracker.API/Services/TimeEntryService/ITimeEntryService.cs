namespace TimeTracker.API.Services.TimeEntryService
{
    public interface ITimeEntryService
    {
        Task<TimeEntryResponse?> GetTimeEntryById(int id);
        Task<List<TimeEntryResponse>> GetAllTimeEntries();
        Task<List<TimeEntryByProjectIdResponse>> GetAllTimeEntriesByProjectId(int projectId);
        Task<List<TimeEntryResponse>> CreateTimeEntry(TimeEntryCreateRequest timeEntry);
        Task<List<TimeEntryResponse>?> UpdateTimeEntry(int id, TimeEntryUpdateRequest timeEntry);
        Task<List<TimeEntryResponse>?> DeleteTimeEntry(int id);
    }
}
