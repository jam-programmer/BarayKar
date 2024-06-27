using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Component
{
    public class LogoComponent(IHomeFactory factory):ViewComponent
    {
        private readonly IHomeFactory _homeFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _homeFactory.GetLogoAsync();
            return View("/Views/Shared/ViewComponent/LogoSection.cshtml", model);
        }
    }
}
