using Application.Common;
using Application.Common.Record.Employment;
using Application.Factories.Employment;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarayKar.Controllers
{
    public class EmploymentController(IEmploymentFactory factory) : Controller
    {
        private readonly IEmploymentFactory _employmentFactory = factory;
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(Guid Id, CancellationToken cancellation = default)
        {
            var pageModel = await _employmentFactory.GetEmploymentDetailAsync(Id, cancellation);
            return View(pageModel);
        }

        public async Task<IActionResult> Explore([FromQuery] EmploymentFilter filter,
            CancellationToken cancellation = default)
        {
            var pageModel = await _employmentFactory.GetEmploymentsAsync(filter, cancellation);
            return View(pageModel.Data);
        }
        [HttpPost]
        public async Task<IActionResult> SendRequest([FromBody] Guid Id)
        {

            if (!User.Identity!.IsAuthenticated)
            {
                Result result = new();
                result.IsSuccess = false;
                result.Message!.Add("برای ثبت درخواست همکاری وارد حساب کاربری خود شوید.");
                return Ok(result);
            }

                var resultRequest = await _employmentFactory.SendRequestForEmploymentAsync(Id, UserId());
           
            return Ok(resultRequest);
        }
        public Guid UserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            return Guid.Parse(userId!);
        }
    }
}
