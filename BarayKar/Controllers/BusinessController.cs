using Application.Common.Record.Business;
using Application.Factories.Business;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Controllers
{
    public class BusinessController(IBusinessFactory factory) : Controller
    {
        private readonly IBusinessFactory _businessFactory = factory;
        public async Task<IActionResult> Detail(Guid Id)
        {
            ViewBag.Id = Id;
            var pageModel = await _businessFactory.GetBusinessDetailAsync(Id);
            return View(pageModel);
        }
        public async Task<IActionResult> AllBusiness([FromQuery] BusinessFilter filter, CancellationToken cancellation = default)
        {
            var pageModel = await _businessFactory.GetBusinessesAsync(filter, cancellation);
            return View(pageModel.Data);
        }
    }
}
