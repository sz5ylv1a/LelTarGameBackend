//using System.Text.Json;

//namespace LelTarGameBackend.Services
//{
//	public class TurnstileService(HttpClient httpClient)
//	{
//		private readonly HttpClient _httpClient = httpClient;
//		private readonly string _secretKey = "1x0000000000000000000000000000000AA";
//		private const string SiteverifyUrl = "https://challenges.cloudflare.com/turnstile/v0/siteverify";

//		public async Task<TurnstileResponse> ValidateTokenAsync(string token, string remoterip = null)
//		{
//			var parameters = new Dictionary<string, string>
//			{
//				{ "secret", _secretKey },
//				{ "response", token }
//			};

//				if (!string.IsNullOrEmpty(remoterip)) parameters.Add("remoteip", remoterip);

//			var postContent = new FormUrlEncodedContent(parameters);

//			try
//			{
//				var response = await _httpClient.PostAsync(SiteverifyUrl, postContent);
//				var stringContent = await response.Content.ReadAsStringAsync();

//				return JsonSerializer.Deserialize<TurnstileResponse>(stringContent);
//			}
//			catch (Exception ex)
//			{
//				return new TurnstileResponse
//				{
//					Success = false,
//					ErroCodes = new[] { "internal-error" }
//				};
//			}
//		}
//	}
//}
