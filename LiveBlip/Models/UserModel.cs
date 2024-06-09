using System.ComponentModel.DataAnnotations;

namespace LiveBlip.Models;

public class UserModel {
	// fields ----------------------------------------------------------------------------------- //
	[Required]
	[StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
	public string? Username { get; set; }
	[Required]
	public string? Email { get; set; }
	[Required]
	[StringLength(
		30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8
	)]
	public string? Password { get; set; }
	[Required]
	[Compare(nameof(Password))]
	public string? ConfirmPassword { get; set; }
	[Required]
	public string? FirstName { get; set; }
	[Required]
	public string? LastName { get; set; }
	[Required]
	public string? Location { get; set; }
}

