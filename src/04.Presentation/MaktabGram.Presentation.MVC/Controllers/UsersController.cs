using MaktabGram.Domain.ApplicationServices.UserAgg;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using MaktabGram.Domain.Services.UserAgg;
using Microsoft.AspNetCore.Mvc;

namespace MaktabGram.Presentation.MVC.Controllers
{
    public class UsersController(IUserApplicationService userApplicationService)  : Controller
    {
        //private readonly IUserApplicationService userApplicationService;
        //public UsersController()
        //{
        //    userApplicationService = new UserApplicationService();
        //}

        [HttpGet]
        public IActionResult Index()
        {
            var users = userApplicationService.GetUsersSummary();

            return View( users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(RegisterUserInputDto model)
        {
            userApplicationService.Register(model);
            return View("Index");
        }
        [HttpGet]
        public IActionResult Active(int userId)
        {
            userApplicationService.Active(userId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeActive(int userId)
        {
            userApplicationService.DeActive(userId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int userId)
        {
            var result = userApplicationService.GetUpdateUserDetails(userId);

            return View(result);
        }

        [HttpPost]
        public IActionResult Update(UpdateGetUserDto model)
        {
            var result = userApplicationService.Update(model.Id, model);
            if(result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
