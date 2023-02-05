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
				string query = @"SELECT * FROM Walks";
				var walks =  (await db.QueryAsync<Walk>(query, null)).ToList();
				return walks;
			}
		}

		public async Task<Walk> GetWalkAsync(Guid id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("id", id, DbType.Guid, ParameterDirection.Input);

				string query = "SELECT * FROM Walks WHERE Id = @id";
				return await(db.QueryFirstOrDefaultAsync<Walk>(query, parameters));
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
