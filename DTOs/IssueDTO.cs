namespace BugTracker.DTOs;

public class IssueDTO
{
    public long ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public bool Status { get; set; }
    public string? AssignedTo { get; set; }
}
