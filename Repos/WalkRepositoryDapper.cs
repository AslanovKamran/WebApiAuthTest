using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public class WalkRepositoryDapper : IWalkRepository
	{

		private readonly string _connectionString;
		public WalkRepositoryDapper(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<Walk> AddWalkAsync(Walk walk)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var Id = Guid.NewGuid();
				walk.Id = Id;
				string query = @"INSERT INTO Walks (Id, Name, Length, RegionId, WalkDifficultyId) 
								VALUES (@Id, @Name, @Length, @RegionId, @WalkDifficultyId)";

				await db.ExecuteAsync(query, walk);
				return walk;
			}
		}

		public async Task<IEnumerable<Walk>> GetAllWalksAsync()
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				string query = @"
								SELECT wh.*,re.*, wd.* FROM Walks AS wh
								JOIN Regions AS re ON re.Id = wh.RegionId
								JOIN WalkDifficulty AS wd ON wd.Id = wh.WalkDifficultyId";
				var walks =  (await db.QueryAsync<Walk, Region, WalkDifficulty, Walk>(query, (walk, region, walkDifficulty) => 
				{
					walk.Region = region;
					walk.WalkDifficulty = walkDifficulty;
					return walk;
				})).ToList();
				return walks;
			}
		}

		public async Task<Walk> GetWalkAsync(Guid id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var p = new { Id = id };
				

				string query = @"SELECT wh.*,re.*, wd.* FROM Walks AS wh
								JOIN Regions AS re ON re.Id = wh.RegionId
								JOIN WalkDifficulty AS wd ON wd.Id = wh.WalkDifficultyId
								WHERE wh.Id = @Id";
				var walk = (await db.QueryAsync<Walk, Region, WalkDifficulty, Walk>(query, (walk, region, walkDifficulty) =>
				{
					walk.Region = region;
					walk.WalkDifficulty = walkDifficulty;
					return walk;
				}, p)).FirstOrDefault();

				return walk!;
			}
		}

		public async Task<Walk> UpdateWalkAsync(Walk walk)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				string query = @"UPDATE Walks SET Name = @Name, Length = @Length, RegionId= @RegionId, WalkDifficultyId= @WalkDifficultyId WHERE Id = @Id";
				await db.ExecuteAsync(query, walk);
				return walk;
			}
		}

		public async Task DeleteWalkAsync(Guid id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				string query = "DELETE FROM Walks Where Walks.Id =@id";
				await db.ExecuteAsync(query, new { id });
			}
		}

	}
}
