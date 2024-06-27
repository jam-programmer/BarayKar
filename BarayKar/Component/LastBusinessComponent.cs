using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Component
{
    public class LastBusinessComponent(IHomeFactory factory):ViewComponent
    {
        private readonly IHomeFactory _homeFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _homeFactory.GetLastBusinessesAsync();
            return View("/Views/Shared/ViewComponent/LastBusinessSection.cshtml", model);
        }
    }
}
