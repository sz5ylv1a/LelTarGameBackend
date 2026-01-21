using LelTarGameBackend.Data;
using LelTarGameBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LelTarGameBackend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DifficultiesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public DifficultiesController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Difficulties
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Difficulties>>> GetDifficulties()
		{
			return await _context.Difficulties.ToListAsync();
		}

		// GET: api/Difficulties/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Difficulties>> GetDifficulties(int id)
		{
			var difficulties = await _context.Difficulties.FindAsync(id);

			if (difficulties == null)
			{
				return NotFound();
			}

			return difficulties;
		}
	}
}
