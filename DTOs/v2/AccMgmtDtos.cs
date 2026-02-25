namespace LelTarGameBackend.DTOs.v2
{
	// account management DTOs
	public record AccMgmtDto(
		long Id,
		string Username,
		string Email,
		string Password,
		int CountryID,
		string Role,
		DateTime CreatedAt
	);
}
