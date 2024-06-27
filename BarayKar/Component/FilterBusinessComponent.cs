using Application.Factories.Business;
using Application.Factories.Employment;
using Microsoft.AspNetCore.Mvc;
using NLog.Config;

namespace BarayKar.Component
{
    public class FilterBusinessComponent(IEmploymentFactory factory):ViewComponent
    {
        private readonly IEmploymentFactory _employmentFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _employmentFactory.GetBusinessFilterAsync();
            return View("/Views/Shared/ViewComponent/FilterBusinessSection.cshtml", model);
        }
    }
}
