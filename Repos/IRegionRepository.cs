using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public interface IRegionRepository
	{
		Task<IEnumerable<Region>> GetAllRegionsAsync();
		Task<Region> GetRegionAsync(Guid id);
		Task<Region> AddRegionAsync(Region region);
		Task DeleteRegionAsync(Guid id);
		Task<Region> UpdateRegionAsync(Region region);
		
	}
}
