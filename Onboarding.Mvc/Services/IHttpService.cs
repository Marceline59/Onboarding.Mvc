using Onboarding.Mvc.Models;

namespace Onboarding.Mvc.Services
{
    public interface IHttpService
    {
        public Task SendReply(HrAnswerRequest reply);
        public Task<List<HrAnswer>> GetReplies();
        public Task SendSurvey(SendSurveyRequest survey);
        public Task<List<Survey>> GetSurveys();
    }
}
