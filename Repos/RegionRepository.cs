using Microsoft.EntityFrameworkCore;
using UdemyCourse.Data;
using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public class RegionRepository : IRegionRepository
	{
		private readonly WalksDbContext _context;

		public RegionRepository(WalksDbContext context)
		=>_context = context;

		public async Task<IEnumerable<Region>> GetAllRegionsAsync()
		=> await _context.Regions.ToListAsync();
	}
}
