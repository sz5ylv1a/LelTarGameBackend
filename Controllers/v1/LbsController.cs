using Asp.Versioning;
using LelTarGameBackend.Data;
using LelTarGameBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LelTarGameBackend.Controllers.v1  // THIS VERSION WILL BE RETIRED SOON AND SHOULD NOT BE USED ANYMORE!!
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[Authorize]
	public class LbsController(AppDbContext context) : ControllerBase
	{
		private readonly AppDbContext _context = context;

		// GET: api/v1/Lbs
		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult<IEnumerable<Lb>>> GetLb()
		{
			return await _context.Lb.ToListAsync();
		}

		// GET: api/v1/Lbs/5
		[HttpGet("{id}")]
		[AllowAnonymous]
		public async Task<ActionResult<Lb>> GetLb(long id)
		{
			var lb = await _context.Lb.FindAsync(id);

			if (lb == null)
			{
				return NotFound();
			}

			return lb;
		}
	}
}
