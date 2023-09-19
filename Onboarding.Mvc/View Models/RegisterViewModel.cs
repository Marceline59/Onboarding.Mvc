using Onboarding.Mvc.Models;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Mvc.View_Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите почту")] public string? Email { get; set; }
        [Required(ErrorMessage = "Укажите пароль")] public string? Password { get; set; }
        public string? Error { get; set; }
    }
}
