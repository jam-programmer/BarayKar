using Application.Common.Record;
using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Controllers
{
    public class IdentityController (IHomeFactory factory): Controller
    {
        private readonly IHomeFactory _homeFactory = factory;

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
                    return Redirect("/");
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



    }
}
