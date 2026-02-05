using Microsoft.EntityFrameworkCore;

namespace CamelRegistry
{
	public class AppDbContext : DbContext
	{
		public DbSet<Camel> Camels { get; set; }
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
	}
}
