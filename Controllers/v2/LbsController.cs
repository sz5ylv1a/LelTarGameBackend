using Asp.Versioning;
using LelTarGameBackend.Models;
using LelTarGameBackend.Data;
using LelTarGameBackend.DTOs.v2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LelTarGameBackend.Controllers.v2
{
	[ApiController]
	[ApiVersion("2.0")]
	[Route("api/v{version:ApiVersion}/lbs")]
	[Authorize(Roles = "Admin,Moderator,User")]
	public class LbsController(AppDbContext context) : ControllerBase
	{
		private readonly AppDbContext _context = context;

		// GET /api/v2/lbs/all
		[HttpGet("all")]
		[Authorize]
		[AllowAnonymous]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _context.Lb.ToListAsync());
		}

		// GET /api/v2/lbs/{id}
		[HttpGet("{id}")]
		[Authorize]
		[AllowAnonymous]
		public async Task<IActionResult> GetSingleEntry(long id)
		{
			var lb = await _context.Lb.FindAsync(id);
			if (lb == null) return NotFound();
			return Ok(lb);
		}

		// POST /api/v2/lbs/submit
		// submits to leaderboard
		[HttpPost("submit")]
		public async Task<IActionResult> SubmitScore(Submit2LbRequest req)
		{
			var makeSureMsg = "Make sure that the score you're submitting is not done on Easy, and is not below 100,000 points.";
			if (req.DifficultyID == 1) return BadRequest(new { message = $"Cannot submit scores on this difficulty! {makeSureMsg}" });
			if (req.Score < 100000) return BadRequest(new { message = $"Score is too low to submit! {makeSureMsg}" });

			var entry = new Lb
			{
				UsernameID = req.UsernameID,
				Score = req.Score,
				DifficultyID = req.DifficultyID
			};

			_context.Lb.Add(entry);
			await _context.SaveChangesAsync();
			return Ok(entry);
		}

		// PUT /api/v2/{id}/disqualify
		// marks score as disqualified (in case of it being caught for cheating)
		[HttpPut("{id}/disqualify")]
		[Authorize(Roles = "Admin,Moderator")]
		public async Task<IActionResult> DisqualifyScore(long id)
		{
			var entry = await _context.Lb.FindAsync(id);
			if (entry == null) return NotFound();

			if (entry.IsDisqualified) {
				entry.IsDisqualified = false;
			}
			else
			{
				entry.IsDisqualified = true;
			}

			await _context.SaveChangesAsync();
			return Ok(new { entry.IsDisqualified });
		}
	}
}
