﻿using Mapster;
using System.Net.Http.Json;
using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services.TimeEntryService
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly HttpClient _http;
        public List<TimeEntryResponse> TimeEntries { get; set; } = new List<TimeEntryResponse>();

        public event Action? OnChange;

        public TimeEntryService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetTimeEntriesByProjectId(int projectId)
        {
            List<TimeEntryResponse>? result;
            if (projectId <= 0)
            {
                result = await _http.GetFromJsonAsync<List<TimeEntryResponse>>("api/timeentry");
            }
            else
            {
                result = await _http.GetFromJsonAsync<List<TimeEntryResponse>>($"api/timeentry/project/{projectId}");
            }

            if (result != null)
            {
                TimeEntries = result;
                OnChange?.Invoke();
            }
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
    }
}
