namespace TimeTracker.Shared.Models.TimeEntry
{
    public record struct TimeEntryCreateRequest(
        int TimeEntryId,
        DateTime Start,
        DateTime? End
        );
}