using System.ComponentModel.DataAnnotations;
namespace LiveBlip.Models
{
    public class SignInModel
    {
        public string Username { get; set; }= string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}


