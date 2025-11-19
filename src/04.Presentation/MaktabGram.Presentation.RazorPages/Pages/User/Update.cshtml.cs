using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.User
{
    public class UpdateModel(IUserApplicationService userApplicationService) : PageModel
    {
        [BindProperty]
        public UpdateGetUserDto model { get; set; }
        public void OnGet(int id)
        {
            model = userApplicationService.GetUpdateUserDetails(id);
        }

        public IActionResult OnPost()
        {
            var result = userApplicationService.Update(model.Id, model);
            if (result.IsSuccess)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
