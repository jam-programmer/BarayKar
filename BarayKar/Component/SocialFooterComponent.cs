using Application.Factories.Business;
using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;
using NLog.Config;

namespace BarayKar.Component
{
    public class SocialFooterComponent(IHomeFactory factory):ViewComponent
    {
        private readonly IHomeFactory _homeFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {
            var model = await _homeFactory.GetSocialsAsync();
            return View("/Views/Shared/ViewComponent/SocialFooterSection.cshtml", model);
        }
    }
}
