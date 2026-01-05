using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.UserAgg.Contracts.User;
using MaktabGram.Presentation.RazorPages.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.Account
{
    public class LoginViewModel
    {
        public string Mobile { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel(IUserApplicationService userApplicationService, ICookieService cookieService) : BasePageModel
    {
        [BindProperty]
        public LoginViewModel Model { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet()
        {

            if (UserIsLoggedIn())
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToPage("/Index", new { area = "Admin" });
                }

                return RedirectToPage("/Profile", new { area = "Account" });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id ,string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var loginResult = await userApplicationService.Login(Model.Mobile, Model.Password, default);

            if (loginResult.IsSuccess)
            {
                TempData["SuccessMessage"] = "ورود با موفقیت انجام شد";
                
                // اگر کاربر ادمین باشه به پنل ادمین برود
                if (User.IsInRole("Admin"))
                {
                    return RedirectToPage("/Index", new { area = "Admin" });
                }

                return RedirectToPage(returnUrl ?? "/Profile", new { area = "Account" });
            }
            else
            {
                ModelState.AddModelError(string.Empty, loginResult.Message);
            }

            return Page();
        }
    }

}
