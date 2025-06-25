namespace BugTracker.Models;

public class Issue
{
    public long ID { get; set; }
    public required string Title { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public bool Status { get; set; }
    public required string Priority { get; set; } = string.Empty;
    public string? CreatedBy { get; set; }
    public string? AssignedTo { get; set; }
    public DateTime Timestamps { get; set; }

}