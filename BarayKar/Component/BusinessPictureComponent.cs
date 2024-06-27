using Application.Factories.Business;
using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;
using NLog.Config;

namespace BarayKar.Component
{
    public class BusinessPictureComponent(IBusinessFactory factory) :ViewComponent
    {
        private readonly IBusinessFactory _businessFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {
            var model = await _businessFactory.GetBusinessPicturesAsync(Id);
            return View("/Views/Shared/ViewComponent/BusinessPictureSection.cshtml", model);
        }
    }
}
