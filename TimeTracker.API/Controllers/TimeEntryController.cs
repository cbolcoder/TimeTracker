﻿using Microsoft.AspNetCore.Mvc;

namespace TimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;

        public TimeEntryController(ITimeEntryService timeEntryService)
        {
            this._timeEntryService = timeEntryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TimeEntryResponse>>> GetAllTimeEntries()
        {
            return Ok(await _timeEntryService.GetAllTimeEntries());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeEntryResponse>> GetTimeEntryById(int id)
        {
            var result = await _timeEntryService.GetTimeEntryById(id);

            if (result is null) return NotFound($"Entity with {id} was not found.");

            return Ok(result);
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<TimeEntryByProjectIdResponse>> GetTimeEntryByProjectId(int projectId)
        {
            var result = await _timeEntryService.GetAllTimeEntriesByProjectId(projectId);

            if (result is null) return NotFound($"Entity with {projectId} was not found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<TimeEntryResponse>>> CreateTimeEntry(TimeEntryCreateRequest timeEntry)
        {
            return Ok(await _timeEntryService.CreateTimeEntry(timeEntry));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<TimeEntryResponse>>> UpdateTimeEntry(int id, TimeEntryUpdateRequest timeEntry)
        {
            var result = await _timeEntryService.UpdateTimeEntry(id, timeEntry);

            if (result is null) return NotFound($"Entity with {id} was not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTimeEntry(int id)
        {
            var result = await _timeEntryService.DeleteTimeEntry(id);

            if (result is null) return NotFound($"Entity with {id} was not found.");

            return Ok(result);
        }
    }
}
