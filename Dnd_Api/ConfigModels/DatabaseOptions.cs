using System.ComponentModel.DataAnnotations;

namespace Dnd_Api.ConfigModels
{
	public class DatabaseOptions
	{
		[Required]
		public string Default { get; set; } = null!;
	}
}
