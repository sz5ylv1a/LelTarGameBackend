using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LelTarGameBackend.Models.v1
{
	public class Lb // Leaderboards
	{
		[Key]
		public long Id { get; set; }

		[Required]
		[ForeignKey("LbUser")]
		public long UsernameID { get; set; }
		[JsonIgnore]
		public required Users Users { get; set; }

		[Required]
		public long Score { get; set; }

		[Required]
		[ForeignKey("LbDiff")]
		public int DifficultyID { get; set; }
		[JsonIgnore]
		public required Difficulties Difficulties { get; set; }

		[JsonIgnore]
		public DateTime AchievedAt { get; set; } = DateTime.UtcNow;
	}
}
