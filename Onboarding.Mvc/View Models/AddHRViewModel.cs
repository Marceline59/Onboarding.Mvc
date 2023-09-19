using System.ComponentModel.DataAnnotations;

namespace Onboarding.Mvc.View_Models
{
    public class AddHRViewModel
    {
        [Required(ErrorMessage = "Укажите пароль")] public string? Password { get; set; }
        [Required(ErrorMessage = "Укажите почту")] public string? Email { get; set; }
        public List<string> ListUsers { get; set; } = new List<string>();
        public string? Error { get; set; }
    }
}
