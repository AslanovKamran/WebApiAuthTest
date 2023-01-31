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
	}
}
