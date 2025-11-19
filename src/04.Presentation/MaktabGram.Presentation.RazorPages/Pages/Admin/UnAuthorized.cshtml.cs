using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.Admin
{
    public class UnAuthorizedModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
