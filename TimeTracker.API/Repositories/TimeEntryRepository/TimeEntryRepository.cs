namespace TimeTracker.API.Repositories.TimeEntryRepository
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly DataContext _dataContext;

        public TimeEntryRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<List<TimeEntry>> GetAllTimeEntries()
        {
            return await _dataContext.TimeEntries.ToListAsync();
        }


        public async Task<TimeEntry?> GetTimeEntryById(int id)
        {
            var timeEntry = await _dataContext.TimeEntries.FindAsync(id);

            return timeEntry;
        }
        public async Task<List<TimeEntry>?> GetTimeEntriesByProjectId(int projectId)
        {
            return await _dataContext.TimeEntries
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }
        public async Task<List<TimeEntry>> CreateTimeEntry(TimeEntry timeEntry)
        {
            _dataContext.TimeEntries.Add(timeEntry);
            await _dataContext.SaveChangesAsync();

            return await GetAllTimeEntries();
        }

        public async Task<List<TimeEntry>> UpdateTimeEntry(int id, TimeEntry timeEntry)
        {
            var dbTimeEntry = await _dataContext.TimeEntries.FindAsync(id);

            if (dbTimeEntry is null) throw new EntityNotFoundException($"Entity with {id} was not found.");

            dbTimeEntry.ProjectId = timeEntry.ProjectId;
            dbTimeEntry.Start = timeEntry.Start;
            dbTimeEntry.End = timeEntry.End;
            dbTimeEntry.DateUpdated = DateTime.Now;

            await _dataContext.SaveChangesAsync();

            return await GetAllTimeEntries();
        }
        public async Task<List<TimeEntry>?> DeleteTimeEntry(int id)
        {
            var dbTimeEntry = await _dataContext.TimeEntries.FindAsync(id);

            if (dbTimeEntry is null) return null;

            _dataContext.TimeEntries.Remove(dbTimeEntry);
            await _dataContext.SaveChangesAsync();

            return await GetAllTimeEntries();
        }
    }
}
