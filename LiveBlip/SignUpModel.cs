using System.ComponentModel.DataAnnotations;

namespace LiveBlip.Models;

public class SignUpModel {
	[Required]
	public string regUname { get; set; }
	[Required]
	public string regPword { get; set; }

}
