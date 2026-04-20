using System.ComponentModel.DataAnnotations;

namespace Dnd_Api.ConfigModels
{
	public class JwtOptions
	{
		[Required]
		public string Issuer { get; set; } = null!;

		[Required]
		public string Audience { get; set; } = null!;

		[Required]
		[MinLength(32)]
		public string Secret { get; set; } = null!;

		[Range(1, 1440)]
		public int ExpiresInMinutes { get; set; }
	}
}
