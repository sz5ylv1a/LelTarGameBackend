using System.ComponentModel.DataAnnotations;

namespace LelTarGameBackend.Models
{
	public class Countries
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(128)]
		public required string Name { get; set; }
	}
}
