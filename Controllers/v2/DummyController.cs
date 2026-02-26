using Asp.Versioning;
using LelTarGameBackend.Data.v2;
using LelTarGameBackend.Models.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LelTarGameBackend.Controllers.v2
{
	// this controller is for dummy data that's accessible with GET requests
	[ApiController]
	[ApiVersion("2.0")]
	[Route("api/v{version:ApiVersion}/dummy")]
	[Authorize]
	[AllowAnonymous]
	public class DummyController(AppDbContext context) : ControllerBase
	{
		private readonly AppDbContext _context = context;

		// GET /api/v2/dummy/countries
		[HttpGet("countries")]
		public async Task<IActionResult> GetCountries()
		{
			var countries = await _context.Countries
				.Select(c => new
				{
					c.Id, c.Name, c.Flag
				}).ToListAsync();
			return Ok(countries);
		}

		// GET /api/v2/dummy/countries/{id}
		[HttpGet("countries/{id}")]
		public async Task<IActionResult> GetSpecificCountry(int id)
		{
			var country = await _context.Countries.FindAsync(id);
			if (country == null) return NotFound();
			return Ok(country);
		}

		// GET /api/v2/dummy/difficulties
		[HttpGet("difficulties")]
		public async Task<IActionResult> GetDifficulties()
		{
			var difficulties = await _context.Difficulties
				.Select(c => new
				{
					c.Id, c.DifficultyName, c.Description,
				}).ToListAsync();
			return Ok(difficulties);
		}

		// GET /api/v2/dummy/difficulties/{id}
		[HttpGet("difficulties/{id}")]
		public async Task<IActionResult> GetSpecificDifficulty(int id)
		{
			var difficulty = await _context.Difficulties.FindAsync(id);
			if (difficulty == null) return NotFound();
			return Ok(difficulty);
		}
	}
}
