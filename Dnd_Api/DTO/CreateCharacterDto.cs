using System.ComponentModel.DataAnnotations;

namespace Dnd_Api.DTO
{
	public class CreateCharacterDto
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Range(1, 12)]
		public int ClassId { get; set; }
		public int RaceId { get; set; }
		public int BackgroundId { get; set; }
		public short AlignmentId { get; set; }
		public short SizeId { get; set; }

		public uint Strength { get; set; }
		public uint Dexterity { get; set; }
		public uint Constitution { get; set; }
		public uint Intelligence { get; set; }
		public uint Wisdom { get; set; }
		public uint Charisma { get; set; }
	}
}
