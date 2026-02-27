using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LelTarGameBackend.Models
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
		[AllowNull]
		[StringLength(4)]
		public string? Icon { get; set; }

		[JsonIgnore]
		public ICollection<Lb>? Lb { get; set; }
	}
}
