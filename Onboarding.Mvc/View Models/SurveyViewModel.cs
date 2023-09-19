using Onboarding.Mvc.Models;

namespace Onboarding.Mvc.View_Models
{
    public class SurveyViewModel
    {
        public string? ActiveLink { get; set; }
        public List <Survey>? PreviousSurveys { get; set; }
        
    }
}
