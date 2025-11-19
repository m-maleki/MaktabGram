using MaktabGram.Domain.Core.UserAgg.Contracts;
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
        public IActionResult OnPost()
        {
            userApplicationService.Register(model);
            return RedirectToPage("Index");
        }
    }
}
