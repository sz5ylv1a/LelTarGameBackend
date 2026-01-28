namespace LelTarGameBackend.DTOs.v1
{
	public class UpdatePasswordRequest
	{
		public required string CurrentPassword { get; set; }
		public required string NewPassword { get; set; }
	}
}
