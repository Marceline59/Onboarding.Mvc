using Microsoft.EntityFrameworkCore.Query.Internal;
using Onboarding.Mvc.Models;
using System.Timers;

namespace Onboarding.Mvc.Services
{
    public class ChatsService : IChatsService
    {
        public List<HrAnswer> Chats { get; set; }
        private IHttpService _httpService;
        private System.Timers.Timer aTimer;

        public ChatsService(IHttpService httpService)
        {
            Chats = new List<HrAnswer>();
            _httpService = httpService;
            aTimer = new System.Timers.Timer(10000);
            SetTimer();
        }

        private async void GetChats(Object source, ElapsedEventArgs e)
        {
            try
            {
                Chats = await _httpService.GetReplies();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Connection error: Unable to get a list of conversations. ChatService/GetChats");
            }
        }

        public async Task ManualGetChats() => Chats = await _httpService.GetReplies();

        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(5000);
            aTimer.Elapsed += GetChats;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
    }
}
