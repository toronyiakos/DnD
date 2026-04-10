namespace Dnd_Api.DTO
{
	public class CreateMonsterDto
	{
		public string Name { get; set; } = null!;
		public int ArmorClass { get; set; }
		public int Hp { get; set; }
		public short Speed { get; set; }

		public short Str { get; set; }
		public short Dex { get; set; }
		public short Con { get; set; }
		public short Int { get; set; }
		public short Wis { get; set; }
		public short Cha { get; set; }

		public short SizeId { get; set; }
		public short TypeId { get; set; }
		public short AlignmentId { get; set; }
	}
}
