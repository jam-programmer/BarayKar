using Application.Common.Record.Business;
using Application.Common.Record.Employment;
using Application.Factories.Employment;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Controllers
{
    public class EmploymentController (IEmploymentFactory factory): Controller
    {
        private readonly IEmploymentFactory _employmentFactory = factory;
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(Guid Id,CancellationToken cancellation=default)
        {
            var pageModel = await _employmentFactory.GetEmploymentDetailAsync(Id, cancellation);
            return View(pageModel);
        }

        public async Task<IActionResult> AllEmployment([FromQuery] EmploymentFilter filter, CancellationToken cancellation = default)
        {
            var pageModel = await _employmentFactory.GetEmploymentsAsync(filter, cancellation);
            return View(pageModel.Data);
        }
    }
}
