using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.UserAgg.Contracts.User;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;
using System.Threading.Tasks;

namespace MaktabGram.Presentation.RazorPages.Areas.Admin.Pages.Users
{
    public class UpdateModel(IUserApplicationService userApplicationService) : PageModel
    {
        [BindProperty]
        public UpdateGetUserDto model { get; set; }

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            model = await userApplicationService.GetUpdateUserDetails(id, cancellationToken);
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await userApplicationService.Update(model.Id, model, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "کاربر با موفقیت بروزرسانی شد";
                return RedirectToPage("Index", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }
        }
    }
}
