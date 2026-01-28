using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LelTarGameBackend.Models.v1
{
	public class Difficulties
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(24)]
		public required string DifficultyName { get; set; }
		[StringLength(140)]
		public string? Description { get; set; }

		[JsonIgnore]
		[ForeignKey("LbDiff")]
		public ICollection<Lb>? Leaderboards { get; set; }
	}
}
