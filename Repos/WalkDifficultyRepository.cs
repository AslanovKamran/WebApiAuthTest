using Microsoft.EntityFrameworkCore;
using UdemyCourse.Data;
using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public class WalkDifficultyRepository : IWalkDifficultyRepository
	{
		private readonly WalksDbContext _context;
		public WalkDifficultyRepository(WalksDbContext context) { _context = context; }

		public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties()
		{
			return await _context.WalkDifficulty.ToListAsync();
		}

		public async Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id)
		{
			var result = await _context.WalkDifficulty.FirstOrDefaultAsync(wd => wd.Id == id);
			return result!;
		}

		public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
		{
			walkDifficulty.Id = Guid.NewGuid();
			await _context.AddAsync(walkDifficulty);
			await _context.SaveChangesAsync();
			return walkDifficulty;
		}

		public async Task DeleteWalkDifficultyAsync(Guid id)
		{
			var dataToDelete = await _context.WalkDifficulty.FirstOrDefaultAsync(wd => wd.Id == id);
			if (dataToDelete is not null)
			{
				_context.WalkDifficulty.Remove(dataToDelete!);
				await _context.SaveChangesAsync();
			}
		}


		public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(WalkDifficulty walkDifficulty)
		{
			var walkDifficultyToUpdate = await _context.WalkDifficulty.FirstOrDefaultAsync(wd => wd.Id == walkDifficulty.Id);
			if (walkDifficultyToUpdate is not null)
			{
				walkDifficultyToUpdate.Code = walkDifficulty.Code;
				await _context.SaveChangesAsync();
				return walkDifficultyToUpdate;
			}
			else return null!;
		}
	}
}
