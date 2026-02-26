using Asp.Versioning;
using LelTarGameBackend.Models;
using LelTarGameBackend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LelTarGameBackend.Controllers.v2
{
	[ApiController]
	[ApiVersion("2.0")]
	[Route("api/v{version:ApiVersion}/accMgmt")]
	[Authorize(Roles = "Admin,Moderator,User")]
	public class AccMgmtController(AppDbContext context) : ControllerBase
	{
		private readonly AppDbContext _context = context;

		// GET /api/v2/users
		[HttpGet]
		public async Task<IActionResult> GetAll() {
			var users = await _context.Users
				.Select(u => new
				{
					u.Id,
					u.Username,
					u.Email,
					u.CountryID,
					u.Role,
					u.CreatedAt
				})
				.ToListAsync();
			return Ok(users);
		}

		// PUT /api/v2/{id}/updateUsername
		[HttpPut("{id}/updateUsername")]
		public async Task<IActionResult> UpdateUsername(long id, [FromBody] string username)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();
			if (user.Username == username)
			{
				return BadRequest(new { message = "You cannot change your username to the same one!" });
			}
			else {
				user.Username = username;
			}

			await _context.SaveChangesAsync();
			return Ok(new { user.Id, user.Username });
		}

		// PUT /api/v2/{id}/updateEmail
		[HttpPut("{id}/updateEmail")]
		public async Task<IActionResult> UpdateEmail(long id, [FromBody] string email)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();
			if (user.Email == email)
			{
				return BadRequest(new { message = "You cannot change your e-mail address to the same one!" });
			}
			else
			{
				user.Email = email;
			}

			await _context.SaveChangesAsync();
			return Ok(new { user.Id, user.Email });
		}

		// PUT /api/v2/{id}/updatePassword
		[HttpPut("{id}/updatePassword")]
		public async Task<IActionResult> UpdatePassword(long id, [FromBody] string password)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();
			if (user.Password == password)
			{
				return BadRequest(new { message = "You cannot change your password to the same one!" });
			}
			else
			{
				user.Password = password;
			}

			await _context.SaveChangesAsync();
			return Ok(new { user.Id, user.Password });
		}

		// PUT /api/v2/{id}/updateUsername
		[HttpPut("{id}/updateRole")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateRole(long id, [FromBody] string role)
		{
			if (role != "Admin" && role != "Moderator" && role != "User" && role != "Banned")
			{
				return BadRequest(new { message = "Invalid role!" });
			}

			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();
			if (user.Role == role)
			{
				return BadRequest(new { message = "You cannot change the role to the same one!" });
			}
			else
			{
				user.Role = role;
			}

			await _context.SaveChangesAsync();
			return Ok(new { user.Id, user.Role });
		}
	}
}
