using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public interface IRegionRepository
	{
		Task<IEnumerable<Region>> GetAllRegionsAsync();
	}
}
