using Mapster;
using System.Net.Http.Json;
using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services.TimeEntryService
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly HttpClient _http;
        public List<TimeEntryResponse> TimeEntries { get; set; } = new List<TimeEntryResponse>();

        public event Action? OnChange;

        public TimeSpan TotalDuration { get; set; }

        public TimeEntryService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetTimeEntriesByProject(int projectId)
        {
            List<TimeEntryResponse>? result = null;
            if (projectId <= 0)
            {
                result = await _http.GetFromJsonAsync<List<TimeEntryResponse>>("api/timeentry");
            }
            else
            {
                result = await _http.GetFromJsonAsync<List<TimeEntryResponse>>($"api/timeentry/project/{projectId}");
            }

            SetTimeEntries(result);
        }

        public async Task<TimeEntryResponse> GetTimeEntryById(int id)
        {
            return await _http.GetFromJsonAsync<TimeEntryResponse>($"api/timeentry/{id}");
        }

        public async Task CreateTimeEntry(TimeEntryRequest request)
        {
            await _http.PostAsJsonAsync("api/timeentry/", request.Adapt<TimeEntryCreateRequest>());
        }

        public async Task UpdateTimeEntry(int id, TimeEntryRequest request)
        {
            await _http.PutAsJsonAsync($"api/timeentry/{id}", request.Adapt<TimeEntryUpdateRequest>());
        }

        public async Task DeleteTimeEntry(int id)
        {
            await _http.DeleteAsync($"api/timeentry/{id}");
        }

        public async Task GetTimeEntriesByYear(int year)
        {
            var result = await _http.GetFromJsonAsync<List<TimeEntryResponse>>($"api/timeentry/year/{year}");
            SetTimeEntries(result);
        }

        public async Task GetTimeEntriesByMonth(int month, int year)
        {
            var result = await _http.GetFromJsonAsync<List<TimeEntryResponse>>($"api/timeentry/month/{month}/{year}");
            SetTimeEntries(result);
        }

        public async Task GetTimeEntriesByDay(int day, int month, int year)
        {
            var result = await _http.GetFromJsonAsync<List<TimeEntryResponse>>($"api/timeentry/day/{day}/{month}/{year}");
            SetTimeEntries(result);
        }

        private void SetTimeEntries(List<TimeEntryResponse>? result)
        {
            if (result != null)
            {
                TimeEntries = result;
                CalculateTotalDuration();
                OnChange?.Invoke();
            }
        }

        private TimeSpan CalculateDuration(TimeEntryResponse timeEntry)
        {
            if (timeEntry.End == null || timeEntry.End.Value < timeEntry.Start)
            {
                return new TimeSpan();
            }

            TimeSpan duration = timeEntry.End.Value - timeEntry.Start;

            return duration;
        }

        private void CalculateTotalDuration()
        {
            TotalDuration = new TimeSpan();
            foreach (var timeEntry in TimeEntries)
            {
                TotalDuration += CalculateDuration(timeEntry);
            }
        }
    }
}