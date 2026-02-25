namespace LelTarGameBackend.DTOs.v2
{
	public record LbDto(
		long Id,
		long UsernameID,
		long Score,
		int DifficultyID,
		bool IsDisqualified,
		DateTime AchievedAt
	);

	public record Submit2LbRequest(
		long UsernameID,
		long Score,
		int DifficultyID
	);
}
