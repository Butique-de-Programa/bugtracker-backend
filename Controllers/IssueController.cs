using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using BugTracker.Data;
using BugTracker.Models;
using BugTracker.DTOs;


namespace BugTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IssueController : ControllerBase
{
    private readonly BugTrackerContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<IssueController> _logger;

    public IssueController(BugTrackerContext context, IMapper mapper, ILogger<IssueController> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;

    }
    
    // GET /issue
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Issue>>> GetAll()
    {
        _logger.LogInformation("Getting all issues.");
        var issues = await _context.Issues.ToListAsync();
        return Ok(_mapper.Map<List<IssueDTO>>(issues));
        //return await _context.Issues.ToListAsync();
    }

    // GET /issue/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Issue>> GetById(long id)
    {
        var issue = await _context.Issues.FindAsync(id);

        if (issue == null)
            return NotFound();

        return issue;
    }

    // POST /issue
    [HttpPost]
    public async Task<ActionResult<Issue>> Create(Issue issue)
    {
        _context.Issues.Add(issue);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = issue.ID }, issue);
    }

    // PUT /issue/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Issue issue)
    {
    var existingIssue = await _context.Issues
        .Include(i => i.CreatedBy)
        .FirstOrDefaultAsync(i => i.ID == id);

    if (existingIssue == null)
        return NotFound();

    // Check if the field is update and then update
    if (!string.IsNullOrWhiteSpace(issue.Title) && issue.Title != existingIssue.Title)
        existingIssue.Title = issue.Title;

    if (!string.IsNullOrWhiteSpace(issue.Description) && issue.Description != existingIssue.Description)
        existingIssue.Description = issue.Description;

    if (issue.Status != existingIssue.Status)
        existingIssue.Status = issue.Status;

    if (!string.IsNullOrWhiteSpace(issue.Priority) && issue.Priority != existingIssue.Priority)
        existingIssue.Priority = issue.Priority;

    if (!string.IsNullOrWhiteSpace(issue.AssignedTo) && issue.AssignedTo != existingIssue.AssignedTo)
        existingIssue.AssignedTo = issue.AssignedTo;

    if (issue.Timestamps != default && issue.Timestamps != existingIssue.Timestamps)
        existingIssue.Timestamps = issue.Timestamps;

    await _context.SaveChangesAsync();
    return NoContent();
    }

    // DELETE /issue/5
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var issue = await _context.Issues.FindAsync(id);
        if (issue == null)
            return NotFound();

        _context.Issues.Remove(issue);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}