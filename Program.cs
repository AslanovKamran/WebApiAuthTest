using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using UdemyCourse.Data;
using UdemyCourse.Repos;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UdemyCourse.Tokens;
using Microsoft.OpenApi.Models;

namespace UdemyCourse
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				var securityScheme = new OpenApiSecurityScheme
				{
					Name = "JWT Authentication",
					Description = "Enter a valid JWT Bearer",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					Reference = new OpenApiReference
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				};

				options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{securityScheme, Array.Empty<string>()}
				});
			});


			var _connectionString = builder.Configuration.GetConnectionString("Default");

			#region Dapper
			//builder.Services.AddScoped<IRegionRepository, RegionRepositoryDapper>
			//					(provider => new RegionRepositoryDapper(_connectionString!));
			//builder.Services.AddScoped<IWalkRepository, WalkRepositoryDapper>
			//					(provider => new WalkRepositoryDapper(_connectionString!));
			//builder.Services.AddScoped<IWalkDifficultyRepository, WalkDifficultyRepositoryDapper>
			//					(provider => new WalkDifficultyRepositoryDapper(_connectionString!));
			//builder.Services.AddSingleton<IUserRepository, StaticUserRepository>();
			#endregion

			#region EF Core
			builder.Services.AddScoped<IRegionRepository, RegionRepository>();
			builder.Services.AddScoped<IWalkRepository, WalkRepository>();
			builder.Services.AddScoped<IWalkDifficultyRepository, WalkDifficultyRepository>();

			//Adding Singleton as no DB is available for users for now
			builder.Services.AddScoped<IUserRepository, UserRepository>();

			builder.Services.AddDbContext<WalksDbContext>((options) =>
			{
				options.UseSqlServer(_connectionString);
			});
			#endregion

			//Adding Scoped Life Time as per different request it may vary
			builder.Services.AddScoped<ITokenHandler, Tokens.TokenHandler>();

			builder.Services.AddAutoMapper(typeof(Program).Assembly);

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey
					(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
				};
			});

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}


		//7130
	}
}