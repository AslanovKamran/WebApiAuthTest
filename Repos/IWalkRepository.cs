using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public interface IWalkRepository
	{
		Task<IEnumerable<Walk>> GetAllWalksAsync();
		Task<Walk> GetWalkAsync(Guid id);
		Task<Walk> AddWalkAsync(Walk walk);
		Task DeleteWalkAsync(Guid id);
		Task<Walk> UpdateWalkAsync(Walk walk);
	}
}
