using Microsoft.Extensions.Logging;
using Onboarding.Mvc.Models;

namespace Onboarding.Mvc.Services
{
    public class HttpService : IHttpService     //Создать интерфейс
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HttpService> _logger;
        static private HttpClient _httpClient = new HttpClient();

        public HttpService(IConfiguration configuration, ILogger<HttpService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<HrAnswer>> GetReplies()
        {
            string path = PathConnectionCreator("chats");
            try
            {
                using var response = await _httpClient.GetAsync(path);
                if (!response.IsSuccessStatusCode)
                {
                    Error? error = await response.Content.ReadFromJsonAsync<Error>();
                    _logger.LogError($"Getting chats failed with a status code {response.StatusCode}. Error: {error?.Message}",
                    response.StatusCode, error);
                }
                else
                {
                    List<HrAnswer>? result = await response.Content.ReadFromJsonAsync<List<HrAnswer>>();
                    _logger.LogInformation($"Связь установлена");
                    return result == null ? new List<HrAnswer>() : result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Network failure. Error: {message}", ex.Message);
            }
            return new List<HrAnswer>();
        }

        public async Task<List<Survey>> GetSurveys()
        {
            string path = PathConnectionCreator("surveys");
            try
            {
                using var response = await _httpClient.GetAsync(path);
                if (!response.IsSuccessStatusCode)
                {
                    Error? error = await response.Content.ReadFromJsonAsync<Error>();
                    _logger.LogError($"Getting surveys failed with a status code {response.StatusCode}. Error: {error?.Message}",
                    response.StatusCode, error);
                }
                else
                {
                    List<Survey>? result = await response.Content.ReadFromJsonAsync<List<Survey>>();
                    return result == null ? new List<Survey>() : result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Network failure. Error: {message}", ex.Message);
            }
            return new List<Survey>();

        }

        public async Task SendReply(HrAnswerRequest reply)
        {
            string path = PathConnectionCreator("chats/reply");
            try
            {
                JsonContent content = JsonContent.Create(reply);
                using var response = await _httpClient.PostAsync(path, content);

                if (!response.IsSuccessStatusCode)
                {
                    Error? error = await response.Content.ReadFromJsonAsync<Error>();
                    _logger.LogError($"Sending chats failed with a status code {response.StatusCode}. Error: {error?.Message}",
                    response.StatusCode, error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Network failure. Error: {message}", ex.Message);
            }
        }

        public async Task SendSurvey(SendSurveyRequest survey)
        {
            string path = PathConnectionCreator("surveys/send");
            try
            {
                JsonContent content = JsonContent.Create(survey);
                using var response = await _httpClient.PostAsync(path, content);

                if (!response.IsSuccessStatusCode)
                {
                    Error? error = await response.Content.ReadFromJsonAsync<Error>();
                    _logger.LogError($"Sending surveys failed with a status code {response.StatusCode}. Error: {error?.Message}",
                    response.StatusCode, error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Network failure. Error: {message}", ex.Message);
            }
        }

        private string PathConnectionCreator(string SendingAddress)
        {
            var address = _configuration.GetSection("ApiConnectionSettings")["Address"];
            var port = _configuration.GetSection("ApiConnectionSettings")["Port"];
            return $"http://{address}:{port}/{SendingAddress}";
        }
    }
}
