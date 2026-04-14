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

			// anti duped name and e-mail mechanism
			if (emailCheck) return BadRequest(new { message = "This e-mail address is already in use!" });
			if (userCheck) return BadRequest(new { message = "This username is already in use!" });

			// make sure everything fullfills the name and password length requirements
			// (prolly not the correct way to implement this shit but i'll worry about that later)
			if (req.Username.Length == 0) return BadRequest(new { message = "Please provide an username!" });
			if (req.Username.Length < 3) return BadRequest(new { message = "Username must be at least 3 characters long!" });
			if (req.Username.Length > 32) return BadRequest(new { message = "Username cannot be longer than 32 characters!" });

			if (req.Password.Length == 0) return BadRequest(new { message = "Please provide a password!" });
			if (req.Password.Length < 8) return BadRequest(new { message = "Password must be at least 8 characters long!" });


			var user = new Users
			{
				Username = req.Username,
				Email = req.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(req.Password),
				CountryID = 0,
				Role = "User"
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			var token = _tokenSvc.GenerateToken(user);
			return Ok(new AuthResponse(token, user.Id, user.Username, user.Role));
		}

		// GET /api/v2/auth/login
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginRequest req)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == req.Username);

			if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.Password.ToString()))
			{
				return Unauthorized(new { message = "Invalid username or password." });
			}

			var token = _tokenSvc.GenerateToken(user);
			return Ok(new AuthResponse(token, user.Id, user.Username, user.Role));
		}
	}
}
