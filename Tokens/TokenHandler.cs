using Microsoft.IdentityModel.Tokens;
using System.Text;
using UdemyCourse.Models.Domain;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace UdemyCourse.Tokens
{
	public class TokenHandler : ITokenHandler
	{
		private readonly IConfiguration _config;
		public TokenHandler(IConfiguration config)
		=>_config = config;

		public string CreateToken(User user)
		{

			//Claims
			var claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
			claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
			claims.Add(new Claim(ClaimTypes.Email, user.EmailAdress));
			user.Roles.ForEach((role) =>
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			});

			//Credentials
			var keyFromAppSettings = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(keyFromAppSettings, SecurityAlgorithms.HmacSha256);

			//Creating Token
			var token = new JwtSecurityToken
				(
				_config["Jwt:Issuer"],
				_config["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials:credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);

		}
	}
}
