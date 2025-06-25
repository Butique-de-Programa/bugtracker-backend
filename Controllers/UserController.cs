using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using BugTracker.Models;

namespace BugTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly BugTrackerContext _context;

    public UserController(BugTrackerContext context)
    {
        _context = context;
    }

    // GET /user
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    // GET /user/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(long id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        return user;
    }

    // POST /user
    [HttpPost]
    public async Task<ActionResult<User>> Create(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = user.ID }, user);
    }

    // PUT /user/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, User user)
    {
        if (id != user.ID)
            return BadRequest();

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Users.Any(e => e.ID == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE /user/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
