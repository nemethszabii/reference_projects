using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamelRegistry
{
	public class Camel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[MinLength(3)]
		[MaxLength(100)]
		public string Name { get; set; }
		[Required]
		[MinLength(3)]
		[MaxLength(50)]
		public string Color { get; set; }
		public int HumpCount { get; set; }
		public DateTime LastFed { get; set; } = DateTime.UtcNow;
	}
}