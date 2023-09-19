using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Onboarding.Mvc.Models;
using Onboarding.Mvc.Services;
using Onboarding.Mvc.Support;
using Onboarding.Mvc.View_Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Onboarding.Mvc.Controllers
{
    public class ChatController : Controller
    {
        private readonly IHttpService _httpService;
        private readonly IChatsService _chatService;

        public ChatController(IHttpService httpService, IChatsService chatService)
        {
            _httpService = httpService;
            _chatService = chatService;
        }
        [HttpGet]
        [Authorize]
        public IActionResult SharedChat()
        {
            return View(GenerateViewModel());
        }

        [HttpGet]
        [Authorize]
        public IActionResult Selecting(long userId, string question)
        {
            AnswerViewModel _answerViewModel = GenerateViewModel();
            if (ModelState.IsValid)
            {
                HrAnswer? selectedChat = _answerViewModel.Chats.FirstOrDefault(x => x.Asked.UserId == userId && x.Asked.Question == question);

                if (selectedChat != null)
                {
                    _answerViewModel.Message = $"Выбранный пользователь: {selectedChat.Asked.Nickname}";
                    _answerViewModel.SelectedChat = selectedChat;
                    HttpContext.Session.Set<HrAnswer>("selectedChat", selectedChat);
                }
            }
            else
                _answerViewModel.Message = "Ошибка страницы";

            return View("~/Views/Chat/SharedChat.cshtml", _answerViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendMessage(AnswerViewModel receivedAnswerViewModel)
        {
            AnswerViewModel _answerViewModel = GenerateViewModel();
            if (!string.IsNullOrEmpty(receivedAnswerViewModel.SelectedChat?.Answer) && _answerViewModel.SelectedChat?.Id == Guid.Empty)
            {
                if (_answerViewModel.SelectedChat == null)
                {
                    _answerViewModel.Message = "Сессия истекла";
                    return View("~/Views/Chat/SharedChat.cshtml", _answerViewModel);
                }

                HrAnswerRequest result = new HrAnswerRequest()
                {
                    Answer = receivedAnswerViewModel.SelectedChat.Answer,
                    CreatedAt = DateTime.UtcNow,
                    BotUserQuestionId = _answerViewModel.SelectedChat.Asked.Id
                };

                await _httpService.SendReply(result);
                _answerViewModel.Message = "Отправка завершена";
                HttpContext.Session.Remove("selectedChat");
                _answerViewModel.SelectedChat = null;
                await _chatService.ManualGetChats();
            }
            else
                _answerViewModel.Message = "Некорректный ответ или попытка повторно отправить ответ";

            return View("~/Views/Chat/SharedChat.cshtml", _answerViewModel);
        }
        #region Генерация модели представления
        private AnswerViewModel GenerateViewModel()
        {
            return new AnswerViewModel() { SelectedChat = GenerateSelectedChat(), Chats = GenerateChats() };
        }

        private List<HrAnswer> GenerateChats()
        {
            List<HrAnswer> chats = _chatService.Chats == null ? new List<HrAnswer>() : _chatService.Chats;
            return chats;
        }
        private HrAnswer? GenerateSelectedChat() => HttpContext.Session.Get<HrAnswer>("selectedChat");
        #endregion
    }
}