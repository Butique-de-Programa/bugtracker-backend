using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using BugTracker.Models;

namespace BugTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class IssueController : ControllerBase
{
    private readonly BugTrackerContext _context;

    public IssueController(BugTrackerContext context)
    {
        _context = context;
    }

    // GET /issue
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Issue>>> GetAll()
    {
        return await _context.Issues.ToListAsync();
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
        if (id != issue.ID)
            return BadRequest();

        _context.Entry(issue).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Issues.Any(e => e.ID == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE /issue/5
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
