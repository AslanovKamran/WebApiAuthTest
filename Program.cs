using Microsoft.EntityFrameworkCore;
using UdemyCourse.Data;
using UdemyCourse.Repos;

namespace UdemyCourse
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var _connectionString = builder.Configuration.GetConnectionString("Default");

			#region Dapper
			//builder.Services.AddScoped<IRegionRepository, RegionRepositoryDapper>
			//					(provider => new RegionRepositoryDapper(_connectionString!));
			//builder.Services.AddScoped<IWalkRepository, WalkRepositoryDapper>
			//					(provider => new WalkRepositoryDapper(_connectionString!));
			#endregion

			#region EF Core
			builder.Services.AddScoped<IRegionRepository, RegionRepository>();
			builder.Services.AddScoped<IWalkRepository, WalkRepository>();

			builder.Services.AddDbContext<WalksDbContext>((options) =>
			{
				options.UseSqlServer(_connectionString);
			});
			#endregion


			builder.Services.AddAutoMapper(typeof(Program).Assembly);

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}