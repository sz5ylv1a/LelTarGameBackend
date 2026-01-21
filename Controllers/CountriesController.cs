using LelTarGameBackend.Data;
using LelTarGameBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LelTarGameBackend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountriesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public CountriesController(AppDbContext context)
		{
			_context = context;
		}

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
