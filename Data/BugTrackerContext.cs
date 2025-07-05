using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BugTracker.Models;

namespace BugTracker.Data;

public class BugTrackerContext : IdentityDbContext<User>
{
    public BugTrackerContext(DbContextOptions<BugTrackerContext> options)
        : base(options) { }

    public DbSet<Issue> Issues { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
}