using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public interface IUserRepository
	{
		Task<User> AuthenticateAsync(string userName, string password);
	}
}
