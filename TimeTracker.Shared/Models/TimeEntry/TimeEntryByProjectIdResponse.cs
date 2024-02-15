using TimeTracker.Shared.Models.Project;

namespace TimeTracker.Shared.Models.TimeEntry
{
    public record struct TimeEntryByProjectIdResponse(
        int Id,
        DateTime Start,
        DateTime? End
        );
}
