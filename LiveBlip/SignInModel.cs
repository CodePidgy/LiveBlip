using System.ComponentModel.DataAnnotations;
namespace LiveBlip.Models
{
    public class SignInModel
    {
		[Required]
        public string Username { get; set; }
		[Required]
        public string Password { get; set; }

    }
}


