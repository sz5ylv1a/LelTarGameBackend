using Asp.Versioning;
using LelTarGameBackend.Data.v1;
using LelTarGameBackend.Models.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LelTarGameBackend.Controllers.v1
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion("1.0")]
	[Authorize]
	[AllowAnonymous]
	public class CountriesController(AppDbContext context) : ControllerBase
	{
		private readonly AppDbContext _context = context;

		// GET: api/Countries
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Countries>>> GetCountries()
		{
			return await _context.Countries.ToListAsync();
		}

		// GET: api/Countries/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Countries>> GetCountries(int id)
		{
			var countries = await _context.Countries.FindAsync(id);

			if (countries == null)
			{
				return NotFound();
			}

			return countries;
		}
	}
}
