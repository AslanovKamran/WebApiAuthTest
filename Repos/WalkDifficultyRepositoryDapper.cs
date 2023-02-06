using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public class WalkDifficultyRepositoryDapper : IWalkDifficultyRepository
	{
		private string _connectionString;
		public WalkDifficultyRepositoryDapper(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties()
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				string query = "SELECT * FROM WalkDifficulty";
				return (await db.QueryAsync<WalkDifficulty>(query, null)).ToList();
			}
		}

		public async Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("id", id, DbType.Guid, ParameterDirection.Input);

				string query = "SELECT * FROM WalkDifficulty WHERE Id = @id";
				return await (db.QueryFirstOrDefaultAsync<WalkDifficulty>(query, parameters));
			}
		}

		public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var Id = Guid.NewGuid();
				walkDifficulty.Id = Id;
				string query = @"INSERT INTO WalkDifficulty (Id, Code)
									VALUES (@Id, @Code)";

				await db.ExecuteAsync(query, walkDifficulty);
				return walkDifficulty;
			}
		}

		public async Task DeleteWalkDifficultyAsync(Guid id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				string query = "DELETE FROM WalkDifficulty Where WalkDifficulty.Id = @id";
				await db.ExecuteAsync(query, new { id });
			}
		}

		public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(WalkDifficulty walkDifficulty)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				string query = @"UPDATE WalkDifficulty SET Code = @Code WHERE Id = @id";
				await db.ExecuteAsync(query, walkDifficulty);
				return walkDifficulty;
			}
		}
	}
}
