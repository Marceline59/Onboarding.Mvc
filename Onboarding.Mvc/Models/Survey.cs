using System.ComponentModel.DataAnnotations;
namespace Onboarding.Mvc.Models
{
    public class Survey : BaseStoredModel
    {
        public List<long>? UserIds { get; init; }
        [Required] public string Link { get; init; } = null!;
        public bool? IsBroadcast { get; init; }
    }
}
