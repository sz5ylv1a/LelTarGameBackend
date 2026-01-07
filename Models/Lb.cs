using System.ComponentModel.DataAnnotations;

namespace LelTarGameBackend.Models
{
	public class Lb // Leaderboards
	{
		[Key]
		public long Id { get; set; }
		public long UsernameID { get; set; }
		public long Score { get; set; }
		public int DifficultyID { get; set; }
		public DateTime AchievedAt { get; set; } = DateTime.UtcNow;
	}
}
