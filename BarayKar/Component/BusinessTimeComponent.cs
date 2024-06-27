using Application.Factories.Business;
using Microsoft.AspNetCore.Mvc;
using NLog.Config;

namespace BarayKar.Component
{
    public class BusinessTimeComponent(IBusinessFactory factory) :ViewComponent
    {
        private readonly IBusinessFactory _businessFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {
            var model = await _businessFactory.GetBusinessTimesAsync(Id);
            return View("/Views/Shared/ViewComponent/BusinessTimeSection.cshtml", model);
        }
    }
}
