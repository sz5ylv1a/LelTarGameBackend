using Asp.Versioning;
using LelTarGameBackend.Data.v1;
using LelTarGameBackend.Models.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LelTarGameBackend.Controllers.v1
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion("1.0")]
	public class LbsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public LbsController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Lbs
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Lb>>> GetLb()
		{
			return await _context.Lb.ToListAsync();
		}

		// GET: api/Lbs/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Lb>> GetLb(long id)
		{
			var lb = await _context.Lb.FindAsync(id);

			if (lb == null)
			{
				return NotFound();
			}

			return lb;
		}

		// PUT: api/Lbs/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutLb(long id, Lb lb)
		{
			if (id != lb.Id)
			{
				return BadRequest();
			}

			_context.Entry(lb).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!LbExists(id))
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

		// POST: api/Lbs
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Lb>> PostLb(Lb lb)
		{
			_context.Lb.Add(lb);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetLb", new { id = lb.Id }, lb);
		}

		// DELETE: api/Lbs/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteLb(long id)
		{
			var lb = await _context.Lb.FindAsync(id);
			if (lb == null)
			{
				return NotFound();
			}

			_context.Lb.Remove(lb);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool LbExists(long id)
		{
			return _context.Lb.Any(e => e.Id == id);
		}
	}
}
