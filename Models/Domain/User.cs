using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyCourse.Models.Domain
{
	public class User
	{
		public Guid Id { get; set; }
		public string UserName { get; set; } = string.Empty;
		public string EmailAdress { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		[NotMapped]
		public List<string> Roles { get; set; } = new();
		public string FirstName { get; set; } = string.Empty;
		public string LastName{ get; set; } = string.Empty;

		//NavigationProperty
		public IEnumerable<User_Role>? UserRoles { get; set; } 
	}
}
