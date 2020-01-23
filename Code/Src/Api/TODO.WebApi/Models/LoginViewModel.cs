using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TODO.Web.Models
{
    public class LoginViewModel
    {       
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
