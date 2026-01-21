namespace LelTarGameBackend.DTOs.v1
{
	public class UpdatePasswordRequest
	{
		public string CurrentPassword { get; set; }
		public string NewPassword { get; set; }
	}
}
