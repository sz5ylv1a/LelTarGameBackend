using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
		[AllowNull]
		[StringLength(4)]
		public string? Icon { get; set; }
	}
}
