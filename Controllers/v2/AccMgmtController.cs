using Asp.Versioning;
using LelTarGameBackend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace LelTarGameBackend.Controllers.v2
{
	[ApiController]
	[ApiVersion("2.0")]
	[Route("api/v{version:ApiVersion}/accMgmt")]
	[Authorize(Roles = "Admin,Moderator,User")]
	public class AccMgmtController(AppDbContext context) : ControllerBase
	{
		private readonly AppDbContext _context = context;

		// GET /api/v2/accMgmt/view/all
		[HttpGet("view/all")]
		[Authorize]
		[AllowAnonymous]
		public async Task<IActionResult> GetAll() {
			var users = await _context.Users
				.Select(u => new
				{
					u.Id,
					u.Username,
					u.CountryID,
					u.Role,
					u.CreatedAt
				})
				.ToListAsync();
			return Ok(users);
		}

		// GET /api/v2/accMgmt/view/{id}
		[HttpGet("view/{id}")]
		[Authorize]
		[AllowAnonymous]
		public async Task<IActionResult> GetSpecificInfo(long id)
		{
			var user = await _context.Users
				.Select(u => new
				{
					u.Id,
					u.Username,
					u.CountryID,
					u.Role,
					u.CreatedAt
				})
				.FirstOrDefaultAsync(u => u.Id == id);

			if (user == null) return NotFound();
			return Ok(user);
		}

		// PUT /api/v2/accMgmt/{id}/updateUsername
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

		// PUT /api/v2/accMgmt/{id}/updateEmail
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

		// PUT /api/v2/accMgmt/{id}/updatePassword
		[HttpPut("{id}/updatePassword")]
		public async Task<IActionResult> UpdatePassword(long id, [FromBody] string password)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();
			if (BCrypt.Net.BCrypt.Verify(password, user.Password))
			{
				return BadRequest(new { message = "You cannot change your password to the same one!" });
			}
			else
			{
				user.Password = BCrypt.Net.BCrypt.HashPassword(password);
			}

			await _context.SaveChangesAsync();
			return Ok(new { user.Id, user.Password });
		}

		// PUT /api/v2/accMgmt/{id}/updateCountry
		[HttpPut("{id}/updateCountry")]
		public async Task<IActionResult> UpdateCountry(long id, [FromBody] int countryId)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();
			if (user.CountryID == countryId)
			{
				return BadRequest(new { message = "You cannot change your username to the same one!" });
			}
			else
			{
				user.CountryID = countryId;
			}

			await _context.SaveChangesAsync();
			return Ok(new { user.Id, user.CountryID });
		}

		// PUT /api/v2/accMgmt/{id}/updateRole
		// this is for promoting or demoting staff, or to outright ban an user
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

		// DELETE /api/v2/accMgmt/{id}/deleteAccount
		[HttpDelete("{id}/deleteAccount")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteAccount(long id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			_context.Users.Remove(user);

			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}
