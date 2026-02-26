using Asp.Versioning;
using LelTarGameBackend.Data;
using LelTarGameBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LelTarGameBackend.Controllers.v1
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[Authorize]
	public class UsersController(AppDbContext context) : ControllerBase
	{
		private readonly AppDbContext _context = context;

		// GET: api/Users
		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
		{
			return await _context.Users.ToListAsync();
		}

		// GET: api/Users/5
		[HttpGet("{id}")]
		[AllowAnonymous]
		public async Task<ActionResult<Users>> GetUsers(long id)
		{
			var users = await _context.Users.FindAsync(id);

			if (users == null)
			{
				return NotFound();
			}

			return users;
		}

		// PUT: api/Users/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> PutUsers(long id, Users users)
		{
			if (id != users.Id)
			{
				return BadRequest();
			}

			_context.Entry(users).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UsersExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Users
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Users>> PostUsers(Users users)
		{
			_context.Users.Add(users);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetUsers", new { id = users.Id }, users);
		}

		// DELETE: api/Users/5
		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> DeleteUsers(long id)
		{
			var users = await _context.Users.FindAsync(id);
			if (users == null)
			{
				return NotFound();
			}

			_context.Users.Remove(users);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool UsersExists(long id)
		{
			return _context.Users.Any(e => e.Id == id);
		}
	}
}
