using Microsoft.EntityFrameworkCore;
using UdemyCourse.Data;
using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public class WalkRepository : IWalkRepository
	{
		private readonly WalksDbContext _context;
		public WalkRepository(WalksDbContext context)
		=> _context = context;

		public async Task<IEnumerable<Walk>> GetAllWalksAsync()
		{
			var result = await _context.Walks
				.Include(x => x.Region)
				.Include(x => x.WalkDifficulty)
				.ToListAsync();
			return result;
		}

		public async Task<Walk> GetWalkAsync(Guid id)
		{
			var result = await _context.Walks
				.Include(x => x.Region)
				.Include(x => x.WalkDifficulty)
				.FirstOrDefaultAsync(x => x.Id == id);
			return result!;

		}

		public async Task<Walk> AddWalkAsync(Walk walk)
		{
			walk.Id = Guid.NewGuid();
			await _context.Walks.AddAsync(walk);
			await _context.SaveChangesAsync();
			return walk;
		}

		public async Task<Walk> UpdateWalkAsync(Walk walk)
		{
			var walkToUpdate = await _context.Walks.FirstOrDefaultAsync(w => w.Id == walk.Id);

			if (walkToUpdate is not null)
			{
				walkToUpdate.Name = walk.Name;
				walkToUpdate.Length = walk.Length;
				walkToUpdate.RegionId = walk.RegionId;
				walkToUpdate.WalkDifficultyId = walk.WalkDifficultyId;

				await _context.SaveChangesAsync();
				return walkToUpdate;
			}
			else return null!;
		}

		public async Task DeleteWalkAsync(Guid id)
		{
			var walkToDelete = await _context.Walks.FirstOrDefaultAsync(w => w.Id == id);
			_context.Walks.Remove(walkToDelete!);
			await _context.SaveChangesAsync();
		}


	}
}
