using Microsoft.EntityFrameworkCore;
using NextQuest.Models;

namespace NextQuest.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options) { }

		public DbSet<User> Users => Set<User>();
		public DbSet<Game> Games => Set<Game>();
		public DbSet<Company> Companies => Set<Company>();
	}
}
