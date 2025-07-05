namespace BugTracker.DTOs;

public class ProjectDTO
{
    public long ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
