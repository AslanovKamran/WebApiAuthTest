using UdemyCourse.Models.Domain;

namespace UdemyCourse.Tokens
{
	public interface ITokenHandler
	{
		string CreateToken(User user);
	}
}
