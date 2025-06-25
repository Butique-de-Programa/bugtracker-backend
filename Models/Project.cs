namespace BugTracker.Models;

public class Project 
{
    public long ID { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}