using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LelTarGameBackend.Models.v1
{
	public class Countries
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(128)]
		public required string Name { get; set; }
		[AllowNull]
		[MaxLength(4)]
		public string? Flag { get; set; }
	}
}
