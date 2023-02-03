using Microsoft.EntityFrameworkCore;
using UdemyCourse.Data;
using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public class RegionRepository : IRegionRepository
	{
		private readonly WalksDbContext _context;

		public RegionRepository(WalksDbContext context)
		=> _context = context;

		public async Task<IEnumerable<Region>> GetAllRegionsAsync()
		=> await _context.Regions.ToListAsync();

		public async Task<Region> GetRegionAsync(Guid id)
		{
			var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
			return region!;
		}

		public async Task<Region> AddRegionAsync(Region region)
		{
			region.Id = Guid.NewGuid();
			await _context.Regions.AddAsync(region);
			await _context.SaveChangesAsync();
			return region;
		}

		public async Task DeleteRegionAsync(Guid id)
		{
			var regionToDelete = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
			if (regionToDelete != null)
			{
				_context.Regions.Remove(regionToDelete);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<Region> UpdateRegionAsync(Region region)
		{
			var regionToUpdate = await _context.Regions.FirstOrDefaultAsync(r => r.Id == region.Id);
			if (regionToUpdate is not null)
			{
				regionToUpdate.Code = region.Code;
				regionToUpdate.Name = region.Name;
				regionToUpdate.Area = region.Area;
				regionToUpdate.Lat = region.Lat;
				regionToUpdate.Long = region.Long;
				regionToUpdate.Population = region.Population;

				await _context.SaveChangesAsync();
				return regionToUpdate;
			}
			else return null!;
			
		}
	}
}
