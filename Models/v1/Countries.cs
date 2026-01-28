using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

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

		[JsonIgnore]
		[ForeignKey("Country")]
		public ICollection<Users>? Users { get; set; }
	}
}
