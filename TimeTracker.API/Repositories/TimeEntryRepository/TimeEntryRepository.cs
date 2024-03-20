﻿namespace TimeTracker.API.Repositories.TimeEntryRepository
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public TimeEntryRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<List<TimeEntry>> CreateTimeEntry(TimeEntry timeEntry)
        {
            var user = await _userContextService.GetUserAsync();
            if (user == null)
            {
                throw new EntityNotFoundException("User was not found.");
            }

            timeEntry.User = user;

            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();

            return await GetAllTimeEntries();
        }

        public async Task<List<TimeEntry>?> DeleteTimeEntry(int id)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
                return null;

            var dbTimeEntry = await _context.TimeEntries
                .FirstOrDefaultAsync(t => t.Id == id && t.User.Id == userId);
            if (dbTimeEntry is null)
            {
                return null;
            }

            _context.TimeEntries.Remove(dbTimeEntry);
            await _context.SaveChangesAsync();

            return await GetAllTimeEntries();
        }

        public async Task<List<TimeEntry>> GetAllTimeEntries()
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
                return new List<TimeEntry>();

            return await _context.TimeEntries.Where(t => t.User.Id == userId).ToListAsync();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesByDay(int day, int month, int year)
        {
            var userId = CheckUserId();

            return await _context.TimeEntries
                .Where(te => te.Start.Day == day
                    && te.Start.Month == month
                    && te.Start.Year == year
                    && te.User.Id == userId)
                .ToListAsync();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesByMonth(int month, int year)
        {
            var userId = CheckUserId();

            return await _context.TimeEntries
                .Where(te => te.Start.Month == month && te.Start.Year == year && te.User.Id == userId)
                .ToListAsync();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesByProject(int projectId)
        {
            string? userId = CheckUserId();

            return await _context.TimeEntries
                .Where(te => te.ProjectId == projectId && te.User.Id == userId)
                .ToListAsync();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesByYear(int year)
        {
            var userId = CheckUserId();

            return await _context.TimeEntries
                .Where(te => te.Start.Year == year && te.User.Id == userId)
                .ToListAsync();
        }

        public async Task<TimeEntry?> GetTimeEntryById(int id)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
                return null;

            var timeEntry = await _context.TimeEntries
                .FirstOrDefaultAsync(t => t.Id == id && t.User.Id == userId);
            return timeEntry;
        }

        public async Task<List<TimeEntry>> UpdateTimeEntry(int id, TimeEntry timeEntry)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                throw new EntityNotFoundException("User was not found.");
            }

            var dbTimeEntry = await _context.TimeEntries
                .FirstOrDefaultAsync(t => t.Id == id && t.User.Id == userId);
            if (dbTimeEntry is null)
            {
                throw new EntityNotFoundException($"Entity with ID {id} was not found.");
            }

            dbTimeEntry.ProjectId = timeEntry.ProjectId;
            dbTimeEntry.Start = timeEntry.Start;
            dbTimeEntry.End = timeEntry.End;
            dbTimeEntry.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();

            return await GetAllTimeEntries();
        }

        private string CheckUserId()
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                throw new EntityNotFoundException("User was not found.");
            }

            return userId;
        }
    }
}