using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.API.Services.TimeEntryService
{
    public interface ITimeEntryService
    {
        TimeEntryResponse? GetTimeEntryById(int id);
        List<TimeEntryResponse> GetAllTimeEntries();
        List<TimeEntryResponse> CreateTimeEntry(TimeEntryCreateRequest timeEntry);
        List<TimeEntryResponse>? UpdateTimeEntry(int id, TimeEntryUpdateRequest timeEntry);
        List<TimeEntryResponse>? DeleteTimeEntry(int id);
    }
}
