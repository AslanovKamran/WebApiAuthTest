using Microsoft.EntityFrameworkCore;
using UdemyCourse.Data;
using UdemyCourse.Models.Domain;

namespace UdemyCourse.Repos
{
	public class UserRepository : IUserRepository
	{
		private readonly WalksDbContext _context;
		public UserRepository(WalksDbContext context)
		{
			_context = context;
		}

		public async Task<User> AuthenticateAsync(string userName, string password)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
			
			var userRoles = await _context.UserRoles.Where(x=>x.UserId == user.Id).ToListAsync();
			
			if (userRoles.Any())
			{
				foreach (var userRole in userRoles)
				{
					var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
					if(role != null)
					{
						user.Roles.Add(role.Name);
					}
				}
			}
			
			return user;
		}
	}
}
