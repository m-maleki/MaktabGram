using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.UserAgg.Contracts.User;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using MaktabGram.Presentation.Api.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;
using System.Threading.Tasks;

namespace MaktabGram.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserApplicationService userApplicationService) : ControllerBase
    {

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(CreateUserInputDto input, CancellationToken cancellationToken)
        {
            var userModel = new RegisterUserInputDto
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Mobile = input.Mobile,
                Password = input.Password,
                Username = input.Username,
                Otp = input.Otp,
            };

            var registerResult = await userApplicationService.Register(userModel, cancellationToken);

            if (registerResult.IsSuccess)
            {
                return Ok(registerResult);
            }

            return BadRequest(registerResult);
        }


        [HttpGet(nameof(Login))]
        public async Task<IActionResult> Login(string mobile, string password, CancellationToken cancellationToken)
        {
            var loginResult = await userApplicationService.Login(mobile, password, cancellationToken);


            if (loginResult.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(loginResult.Message);
        }

        [HttpGet(nameof(SendOTP))]
        public async Task<IActionResult> SendOTP(int number, CancellationToken cancellationToken)
        {
           // await userApplicationService.SendRegisterOtp(mobile, cancellationToken);
            return Ok();
        }
    }
}
