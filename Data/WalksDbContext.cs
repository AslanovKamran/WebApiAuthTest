using Microsoft.EntityFrameworkCore;
using UdemyCourse.Models.Domain;

namespace UdemyCourse.Data
{
	public class WalksDbContext : DbContext
	{
		public WalksDbContext(DbContextOptions<WalksDbContext> options) : base(options)
		{
		}

		public DbSet<Region>? Regions{ get; set; }
		public DbSet<Walk>? Walks{ get; set; }
		public DbSet<WalkDifficulty>? WalkDifficulty{ get; set; }
	}
}
