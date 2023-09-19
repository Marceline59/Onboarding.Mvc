using System.ComponentModel.DataAnnotations;

namespace Onboarding.Mvc.Models
{
    public class BotUserQuestion: BaseStoredModel
    {
        [Required] public long UserId { get; init; }
        [Required] public string Nickname { get; init; } = null!;
        [Required] public string Question { get; init; } = null!;
    }
}
