using Mapster;

namespace TimeTracker.API.Services.TimeEntryService
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly ITimeEntryRepository _timeEntryRepo;

        public TimeEntryService(ITimeEntryRepository timeEntryRepo)
        {
            _timeEntryRepo = timeEntryRepo;
        }

        public async Task<List<TimeEntryResponse>> CreateTimeEntry(TimeEntryCreateRequest request)
        {
            var newEntry = request.Adapt<TimeEntry>();
            var result = await _timeEntryRepo.CreateTimeEntry(newEntry);
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<List<TimeEntryResponse>?> DeleteTimeEntry(int id)
        {
            var result = await _timeEntryRepo.DeleteTimeEntry(id);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<List<TimeEntryResponse>> GetAllTimeEntries()
        {
            var result = await _timeEntryRepo.GetAllTimeEntries();
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<List<TimeEntryResponse>> GetTimeEntriesByDay(int day, int month, int year)
        {
            var result = await _timeEntryRepo.GetTimeEntriesByDay(day, month, year);
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<List<TimeEntryResponse>> GetTimeEntriesByMonth(int month, int year)
        {
            var result = await _timeEntryRepo.GetTimeEntriesByMonth(month, year);
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<List<TimeEntryResponse>> GetTimeEntriesByProject(int projectId)
        {
            var result = await _timeEntryRepo.GetTimeEntriesByProject(projectId);
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<List<TimeEntryResponse>> GetTimeEntriesByYear(int year)
        {
            var result = await _timeEntryRepo.GetTimeEntriesByYear(year);
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public async Task<TimeEntryResponse?> GetTimeEntryById(int id)
        {
            var result = await _timeEntryRepo.GetTimeEntryById(id);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<TimeEntryResponse>();
        }

        public async Task<List<TimeEntryResponse>?> UpdateTimeEntry(int id, TimeEntryUpdateRequest request)
        {
            try
            {
                var updatedEntry = request.Adapt<TimeEntry>();
                var result = await _timeEntryRepo.UpdateTimeEntry(id, updatedEntry);
                return result.Adapt<List<TimeEntryResponse>>();
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }
    }
}