using System.ComponentModel.DataAnnotations;

namespace CamelRegistry
{
	public class CreateCamelDto
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
		[Required]
		[MaxLength(50)]
		public string Color { get; set; }
		public int HumpCount { get; set; }
		public DateTime LastFed { get; set; } = DateTime.UtcNow;
	}
}
