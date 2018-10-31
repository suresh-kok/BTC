using System.ComponentModel.DataAnnotations;

namespace Travel_Request_System_EF.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}