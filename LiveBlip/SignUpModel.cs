using System.ComponentModel.DataAnnotations;

namespace LiveBlip.Models;

public class SignUpModel {
	[Required]
	public string Username { get; set; }
	[Required]
	public string Password { get; set; }
	[Required]
	public string confirmPassword { get; set; }
	[Required]
	public string Email { get; set; }


}
