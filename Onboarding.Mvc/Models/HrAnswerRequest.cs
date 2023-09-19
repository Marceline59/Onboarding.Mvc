using System.ComponentModel.DataAnnotations;

namespace Onboarding.Mvc.Models
{
    public class HrAnswerRequest
    {
        [Required] public Guid BotUserQuestionId { get; init; }
        [Required] public string Answer { get; init; } = null!;
        [Required] public DateTime CreatedAt { get; init; }
    }
}
