using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using LelTarGameBackend.Models.v1;

namespace LelTarGameBackend.Services;

public class TokenService(IConfiguration config)
{
	private readonly IConfiguration _config = config;

	public string GenerateToken(Users user)
	{
		var key = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role)
		};

		var token = new JwtSecurityToken(
			issuer: _config["Jwt:Issuer"],
			audience: _config["Jwt:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddDays(7),
			signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
