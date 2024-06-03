using System.ComponentModel.DataAnnotations;

namespace LiveBlip.Models;
	public class UserModel
		{
			public string Username { get; set; }
			public string Email { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
        	public string Location { get; set; }
		}

