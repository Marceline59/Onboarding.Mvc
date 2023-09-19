using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Onboarding.Mvc.Models;
using Onboarding.Mvc.View_Models;

namespace Onboarding.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Hr> _userManager;
        private readonly SignInManager<Hr> _signInManager;

        public AccountController(UserManager<Hr> userManager, SignInManager<Hr> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var _signInUser = await _signInManager.PasswordSignInAsync(
                    registerViewModel.Email, registerViewModel.Password,
                    false, false);

                if (!_signInUser.Succeeded)
                {
                    registerViewModel.Error = "Неверный логин или пароль";
                    return View(registerViewModel);
                }
                else
                {
                    registerViewModel.Error = null;
                    return RedirectToAction("HRAdd", "Account");
                }
            }
            registerViewModel.Error = "Некорректная авторизация";
            return View(registerViewModel);
        }
        [HttpGet]
        [Authorize]
        public IActionResult HRAdd()
        {
            List<string>? Emails = _userManager.Users.Select(x => x.Email).ToList();
            if (Emails == null) Emails = new List<string>();
            return View(new AddHRViewModel() { ListUsers = Emails });
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> HRAdd(AddHRViewModel addHRViewModel)
        {
            if (ModelState.IsValid)
            {
                var foundUser = await _userManager.FindByEmailAsync(addHRViewModel.Email);
                if (foundUser == null)
                {
                    var result = await _userManager.CreateAsync(new Hr()
                    {
                        Email = addHRViewModel.Email,
                        UserName = addHRViewModel.Email
                    }, addHRViewModel.Password);

                    if (!result.Succeeded)
                        addHRViewModel.Error = "Некорректный логин или пароль";
                    else
                        addHRViewModel.Error = "Пользователь добавлен";
                }
                else
                    addHRViewModel.Error = "Пользователь сущесвтует";
            }
            else
                addHRViewModel.Error = "Ошибка страницы";
            addHRViewModel.ListUsers = _userManager.Users.Select(x => x.Email).ToList();
            if (addHRViewModel.ListUsers == null) addHRViewModel.ListUsers = new List<string>();
            return View(addHRViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Out()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Account");
        }
    }
}
