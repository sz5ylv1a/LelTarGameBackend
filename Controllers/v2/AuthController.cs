using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LelTarGameBackend.Services;
using LelTarGameBackend.Models;
using LelTarGameBackend.Data;
using LelTarGameBackend.DTOs.v2;

namespace LelTarGameBackend.Controllers.v2
{
	[ApiController]
	[ApiVersion("2.0")]
	[Route("api/v{version:ApiVersion}/auth")]
	[Authorize]
	[AllowAnonymous]
	public class AuthController(AppDbContext context, TokenService tokenSvc) : ControllerBase
	{
		private readonly AppDbContext _context = context;
		private readonly TokenService _tokenSvc = tokenSvc;

		// POST /api/v2/auth/register
		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterRequest req)
		{
			var emailCheck = await _context.Users.AnyAsync(u => u.Email == req.Email);
			var userCheck = await _context.Users.AnyAsync(u => u.Username == req.Username);


			if (emailCheck) return BadRequest(new { message = "This e-mail address is already in use!" });
			if (userCheck) return BadRequest(new { message = "This username is already in use!" });

			var user = new Users
			{
				Username = req.Username,
				Email = req.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(req.Password),
				Role = "User"
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			var token = _tokenSvc.GenerateToken(user);
			return Ok(new AuthResponse(token, user.Username, user.Role));
		}

		// GET /api/v2/auth/login
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginRequest req)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == req.Username);

			if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.Password))
			{
				return Unauthorized(new { message = "Invalid username or password." });
			}

			var token = _tokenSvc.GenerateToken(user);
			return Ok(new AuthResponse(token, user.Username, user.Role));
		}
	}
}
