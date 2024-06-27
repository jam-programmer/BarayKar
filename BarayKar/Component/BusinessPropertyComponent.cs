using Application.Factories.Business;
using Microsoft.AspNetCore.Mvc;
using NLog.Config;

namespace BarayKar.Component
{
    public class BusinessPropertyComponent(IBusinessFactory factory) :ViewComponent
    {
        private readonly IBusinessFactory _businessFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {
            var model = await _businessFactory.GetBusinessPropertiesAsync(Id);
            return View("/Views/Shared/ViewComponent/BusinessPropertySection.cshtml", model);
        }
    }
}
