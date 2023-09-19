using Onboarding.Mvc.Models;

namespace Onboarding.Mvc.Services
{
    public interface IChatsService
    {
        public List<HrAnswer> Chats { get; set; }
        public Task ManualGetChats();
    }
}
