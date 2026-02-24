using System.ComponentModel.DataAnnotations;

namespace LelTarGameBackend.Models.v1
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

		public int CountryID { get; set; }
		[Required]
		[StringLength(32)]
		public required string Role { get; set; } = "Default";	// see README.md for possible roles

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
