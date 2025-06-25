namespace BugTracker.Models;

public class User 
{
    public long ID { get; set; }
    public required string Email { get; set; } = string.Empty;
    public required string PasswordHash { get; set; } = string.Empty;
    public string? Role { get; set; } = string.Empty;
}