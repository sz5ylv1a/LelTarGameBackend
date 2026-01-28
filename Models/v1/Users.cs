using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

		[ForeignKey("Country")]
		public int CountryID { get; set; }
		[JsonIgnore]
		public Countries? Countries { get; set; }

		[JsonIgnore]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[JsonIgnore]
		[ForeignKey("LbUser")]
		public ICollection<Lb>? Leaderboards { get; set; }
	}
}
