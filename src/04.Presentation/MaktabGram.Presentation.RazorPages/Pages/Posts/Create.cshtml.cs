using MaktabGram.Domain.ApplicationServices.PostAgg;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Presentation.RazorPages.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.Posts
{
    [Authorize]
    public class CreateModel(IPostApplicationService postApplicationService) : BasePageModel
    {
        [BindProperty]
        public CreatePostInputDto Model { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Model.UserId = (int)GetUserId()!;
            var result = await postApplicationService.Create(Model, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "پست با موفقیت ایجاد شد";
                return RedirectToPage("/Account/Profile");
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }
        }
    }

}
