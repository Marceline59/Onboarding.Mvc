using Onboarding.Mvc.Models;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Mvc.View_Models
{
    public class AnswerViewModel
    {
        public HrAnswer? SelectedChat { get; set; }
        public List<HrAnswer> Chats { get; set; } = new List<HrAnswer>();
        public string? Message { get; set; }
    }
}
