using Asp.Versioning;
using LelTarGameBackend.Data;
using LelTarGameBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LelTarGameBackend.Controllers.v1	// THIS VERSION WILL BE RETIRED SOON AND SHOULD NOT BE USED ANYMORE!!
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
	}
}
