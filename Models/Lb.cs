using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LelTarGameBackend.Models
{
	public class Lb // Leaderboards
	{
		[Key]
		public long Id { get; set; }

		[Required]
		[ForeignKey(nameof(Users))]
		public long UsernameID { get; set; }

		[Required]
		public long Score { get; set; }

		[Required]
		[ForeignKey(nameof(Difficulties))]
		public int DifficultyID { get; set; }
		public bool IsDisqualified { get; set; } = false;

		public DateTime AchievedAt { get; set; } = DateTime.UtcNow;

		[JsonIgnore]
		public Users? Users { get; set; }
		[JsonIgnore]
		public Difficulties? Difficulties { get; set; }
	}
}
