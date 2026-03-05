namespace LelTarGameBackend.DTOs.v2
{
	public record RegisterRequest(
		string Username,
		string Email,
		string Password
	);

	public record LoginRequest(
		string Username,
		string Password
	);

	public record AuthResponse(
		string Token,
		long Id,
		string Username,
		string Role
	);
}
