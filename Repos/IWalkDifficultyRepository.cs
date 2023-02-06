using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public interface IWalkDifficultyRepository
	{
		Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties();
		Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id);
		Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty);
		Task DeleteWalkDifficultyAsync(Guid id);
		Task<WalkDifficulty> UpdateWalkDifficultyAsync(WalkDifficulty walkDifficulty);
	}
}
