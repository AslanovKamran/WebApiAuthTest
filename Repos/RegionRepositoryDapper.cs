using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public class RegionRepositoryDapper : IRegionRepository
	{
		private string _connectionString;
		public RegionRepositoryDapper(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<IEnumerable<Region>> GetAllRegionsAsync()
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				string query = "SELECT * FROM Regions";
				return (await db.QueryAsync<Region>(query, null)).ToList();
			}
		}

		public async Task<Region> GetRegionAsync(Guid id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("id", id, DbType.Guid, ParameterDirection.Input);

				string query = "SELECT * FROM Regions WHERE Id = @id";
				return await (db.QueryFirstOrDefaultAsync<Region>(query, parameters));
			}
		}

		public async Task<Region> AddRegionAsync(Region region)
		{

			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var Id = Guid.NewGuid();
				region.Id = Id;
				string query = @"INSERT INTO Regions (Id, Code, Name, Area, Lat, Long, Population) 
								VALUES (@Id, @Code, @Name, @Area, @Lat, @Long, @Population)";

				await db.ExecuteAsync(query, region);
				return region;
			}

		}

		public async Task DeleteRegionAsync(Guid id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				string query = "DELETE FROM Regions Where Regions.Id =@id";
				await db.ExecuteAsync(query, new { id });
			}
		}

		public async Task<Region> UpdateRegionAsync(Region region)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				string query = @"UPDATE Regions SET Name = @Name, Area = @Area, Lat = @Lat, Long = @Long, Population = @Population WHERE Id = @Id";
				await db.ExecuteAsync(query, region);
				return region;
			}
		}
	}
}
