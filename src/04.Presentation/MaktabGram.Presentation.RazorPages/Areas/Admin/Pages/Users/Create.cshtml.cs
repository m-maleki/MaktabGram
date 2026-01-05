using MaktabGram.Domain.Core.UserAgg.Contracts.User;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.User
{
    public class CreateModel(IUserApplicationService userApplicationService) : PageModel
    {
        [BindProperty]
        public RegisterUserInputDto model { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await userApplicationService.Register(model, cancellationToken);
            
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "کاربر با موفقیت ایجاد شد";
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }
        }
    }
}
