using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;
using NLog.Config;

namespace BarayKar.Component
{
    public class InfoContactComponent(IHomeFactory factory) :ViewComponent
    {
        private readonly IHomeFactory _homeFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {
            var model = await _homeFactory.GetInfoContactAsync();
            return View("/Views/Shared/ViewComponent/InfoContactSection.cshtml", model);
        }
    }
}
