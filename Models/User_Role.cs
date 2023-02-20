using UdemyCourse.Models.Domain;

namespace UdemyCourse.Models
{
	public class User_Role
	{
		public Guid Id { get; set; }

		//Navigation Property
		public Guid UserId { get; set; }
		public User? User { get; set; }

		public Guid RoleId { get; set; }
		public Role? Role { get; set; }
	}
}
