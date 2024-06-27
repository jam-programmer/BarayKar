using Application.Factories.Business;
using Microsoft.AspNetCore.Mvc;
using NLog.Config;

namespace BarayKar.Component
{
    public class FilterProvinceComponent(IBusinessFactory factory):ViewComponent
    {
        private readonly IBusinessFactory _businessFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _businessFactory.GetProvinceFilterAsync();
            return View("/Views/Shared/ViewComponent/FilterProvinceSection.cshtml", model);
        }
    }
}
