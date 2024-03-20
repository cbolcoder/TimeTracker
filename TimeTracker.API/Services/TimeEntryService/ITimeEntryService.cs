﻿namespace TimeTracker.API.Services.TimeEntryService
{
    public interface ITimeEntryService
    {
        Task<TimeEntryResponse?> GetTimeEntryById(int id);
        Task<List<TimeEntryResponse>> GetAllTimeEntries();
        Task<List<TimeEntryResponse>> CreateTimeEntry(TimeEntryCreateRequest timeEntry);
        Task<List<TimeEntryResponse>?> UpdateTimeEntry(int id, TimeEntryUpdateRequest timeEntry);
        Task<List<TimeEntryResponse>?> DeleteTimeEntry(int id);
        Task<List<TimeEntryResponse>> GetTimeEntriesByProject(int projectId);
        Task<List<TimeEntryResponse>> GetTimeEntriesByYear(int year);
        Task<List<TimeEntryResponse>> GetTimeEntriesByMonth(int month, int year);
        Task<List<TimeEntryResponse>> GetTimeEntriesByDay(int day, int month, int year);
    }
}
