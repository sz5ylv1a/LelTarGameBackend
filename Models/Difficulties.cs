using System.ComponentModel.DataAnnotations;

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
	}
}
