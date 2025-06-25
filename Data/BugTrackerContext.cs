using Microsoft.EntityFrameworkCore;
using BugTracker.Models;

namespace BugTracker.Data;

public class BugTrackerContext : DbContext
{
    public BugTrackerContext(DbContextOptions<BugTrackerContext> options)
        : base(options) { }

    public DbSet<Issue> Issues { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}