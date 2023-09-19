using System.ComponentModel.DataAnnotations;

namespace Onboarding.Mvc.Models
{
    public class HrAnswer : BaseStoredModel
    {
        [Required] public BotUserQuestion Asked { get; init; } = null!;

        // We use HrAnswer to return all chats to MVC, so there might be some questions without an answer yet
        public string? Answer { get; init; }
    }
}
