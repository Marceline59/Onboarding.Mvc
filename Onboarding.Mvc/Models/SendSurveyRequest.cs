using System.ComponentModel.DataAnnotations;

namespace Onboarding.Mvc.Models
{
    public class SendSurveyRequest
    {
        public List<long>? UserIds { get; init; }
        [Required] public string Link { get; init; } = null!;
        public bool? IsBroadcast { get; init; }
        [Required] public DateTime CreatedAt { get; init; }
    }
}
