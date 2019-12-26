using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Services.Models
{
	public class UserServiceModel : IMapTo<User>
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string UserName { get; set; }
	}
}
