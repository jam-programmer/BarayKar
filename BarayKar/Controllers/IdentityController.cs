using Application.Common.Record;
using Application.Factories.Home;
using Application.Factories.User;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Controllers
{
    public class IdentityController (IHomeFactory factory, IUserFactory userFactory) : Controller
    {
        private readonly IHomeFactory _homeFactory = factory;
        private readonly IUserFactory _userFactory = userFactory;

        [HttpGet]
        public IActionResult SignIn()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInRecord record ,CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _homeFactory.SignInAsync(record);
                if (result.IsSuccess)
                {
                    return Redirect((string)result.Data!);
                }
                ViewBag.Alert = result.Message!.FirstOrDefault();
            }
            return View(record);
        }
        [HttpGet]
        [Route("/SignOut")]
        public async Task<IActionResult> SignOut()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return Redirect("/");
            }
            await _homeFactory.SignOutAsync();
            return Redirect("/");
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRecord record)
        {
            if (ModelState.IsValid)
            {
                var result = await _homeFactory.SignUpAsync(record);
                if (result.IsSuccess)
                {
                    return Redirect("/");
                }
                ViewBag.Alert = result.Message!.FirstOrDefault();
            }
            return View(record);
        }

        [HttpPost]
        public async Task<IActionResult> SendOtpCode(string userName)
        {
            var result = await _userFactory.SendOtpCode(userName);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> LoginOtp(string userName, string code)
        {
            var result = await _userFactory.CheckOtpCode(userName, code);
            if (result.IsSuccess)
            {
                var resultSignInOtp = await _homeFactory.OtpSignInAsync(userName);
                if (resultSignInOtp.IsSuccess)
                {
                    return Ok(resultSignInOtp);
                }
                else
                {
                    return BadRequest(resultSignInOtp);
                }
            }

            return BadRequest(result);
        }

    }
}
