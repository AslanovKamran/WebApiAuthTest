using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCourse.Models.DTOs;
using UdemyCourse.Repos;
using UdemyCourse.Tokens;

namespace UdemyCourse.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUserRepository _repos;
		private readonly ITokenHandler _tokenHandler;

		public AuthController(IUserRepository repos, ITokenHandler tokenHandler)
		{
			_repos = repos;
			_tokenHandler = tokenHandler;
		}

		[HttpPost]
		[Route("login")]
		public async Task<ActionResult> Login(LoginRequest request)
		{
			var user =   await _repos.AuthenticateAsync(request.UserName, request.Password);
			if(user is not null)
			{
				var token =  _tokenHandler.CreateToken(user);
				return Ok(token);
			}
			return BadRequest("Wrong Credentials");
		}
	}
}
