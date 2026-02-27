using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LelTarGameBackend.Models
{
	public class Users
	{
		[Key]
		public long Id { get; set; }

		[Required]
		[StringLength(32)]
		public required string Username { get; set; }
		[Required]
		[EmailAddress]
		[StringLength(256)]
		public required string Email { get; set; }
		[Required]
		[StringLength(1024)]
		public required string Password { get; set; }

		[ForeignKey(nameof(Countries))]
		public int CountryID { get; set; } = 0;
		[Required]
		[StringLength(32)]
		public required string Role { get; set; } = "User";	// see README.md for possible roles

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		[JsonIgnore]
		public Countries? Countries { get; set; }

		[JsonIgnore]
		public ICollection<Lb>? Lb { get; set; }
	}
}
