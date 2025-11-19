using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using MaktabGram.Presentation.RazorPages.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaktabGram.Presentation.RazorPages.Pages.User
{
    public class IndexModel(IUserApplicationService userApplicationService) : BasePageModel
    {
        public List<GetUserSummaryDto> Users { get; set; }
        //public IActionResult OnGet()
        //{
        //    var userId = GetUserId();
        //    if (userId != null)
        //    {
        //        if (IsAdmin())
        //        {
        //            Users = userApplicationService.GetUsersSummary();
        //            return RedirectToPage("/User/Index");
        //        }
        //        else
        //        {
        //            return RedirectToPage("/Post/Feeds");
        //        }
        //    }
        //    return Page();

        //}

        public void OnGet()
        {
            Users = userApplicationService.GetUsersSummary();
        }


        public IActionResult OnGetActive(int id)
        {
            userApplicationService.Active(id);

            return RedirectToPage("Index");
        }


        public IActionResult OnGetDeActive(int id)
        {
            userApplicationService.DeActive(id);

            return RedirectToPage("Index");
        }
    }
}
