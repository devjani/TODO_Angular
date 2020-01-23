using System.ComponentModel.DataAnnotations;

namespace TODO.Model.Account
{
    public class ForgotPasswordModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
