using System.ComponentModel.DataAnnotations;

namespace LelTarGameBackend.Models.v1
{
	public class Lb // Leaderboards
	{
		[Key]
		public long Id { get; set; }

		[Required]
		public long UsernameID { get; set; }

		[Required]
		public long Score { get; set; }

		[Required]
		public int DifficultyID { get; set; }

		public DateTime AchievedAt { get; set; } = DateTime.UtcNow;
	}
}
