using Application.Factories.Business;
using Microsoft.AspNetCore.Mvc;
using NLog.Config;

namespace BarayKar.Component
{
    public class FilterCategoryComponent(IBusinessFactory factory) :ViewComponent
    {
        private readonly IBusinessFactory _businessFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _businessFactory.GetCategoryFilterAsync();
            return View("/Views/Shared/ViewComponent/FilterCategorySection.cshtml", model);
        }
    }
}
