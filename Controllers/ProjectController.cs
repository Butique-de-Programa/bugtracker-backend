using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using BugTracker.Models;

namespace BugTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly BugTrackerContext _context;

    public ProjectController(BugTrackerContext context)
    {
        _context = context;
    }

    // GET /project
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetAll()
    {
        return await _context.Projects.ToListAsync();
    }

    // GET /project/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetById(long id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
            return NotFound();

        return project;
    }

    // POST /project
    [HttpPost]
    public async Task<ActionResult<Project>> Create(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = project.ID }, project);
    }

    // PUT /project/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Project project)
    {
        if (id != project.ID)
            return BadRequest();

        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Projects.Any(e => e.ID == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE /project/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
            return NotFound();

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
