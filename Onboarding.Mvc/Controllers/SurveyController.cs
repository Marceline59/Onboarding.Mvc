using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onboarding.Mvc.Models;
using Onboarding.Mvc.Services;
using Onboarding.Mvc.View_Models;
using System.Security.Cryptography.X509Certificates;

namespace Onboarding.Mvc.Controllers
{
    public class SurveyController : Controller
    {
        private readonly IHttpService _httpService;
        public SurveyController(IHttpService httpService)
        {
            _httpService = httpService;
        }

        // GET: SurveyController
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SurvPage()
        {
            SurveyViewModel _surveyViewModel = new SurveyViewModel();
            _surveyViewModel.PreviousSurveys = await _httpService.GetSurveys();
            return View(_surveyViewModel);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SurvPage(SurveyViewModel _surveyViewModel)
        {
            if (!string.IsNullOrEmpty(_surveyViewModel.ActiveLink))
            {
                SendSurveyRequest Evgen = new SendSurveyRequest() { Link = _surveyViewModel.ActiveLink, IsBroadcast = true, CreatedAt = DateTime.UtcNow };
                await _httpService.SendSurvey(Evgen);
            }
            return (RedirectToAction("SurvPage", "Survey"));
            //_surveyViewModel.PreviousSurveys = await _httpService.GetSurveys();
            //return View(_surveyViewModel);
        }
    }
}
