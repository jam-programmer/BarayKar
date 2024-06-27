using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Component
{
    public class HeaderComponent(IHomeFactory factory): ViewComponent
    {
        private readonly IHomeFactory _homeFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _homeFactory.GetHeaderAsync();
            return View("/Views/Shared/ViewComponent/HeaderSection.cshtml", model);
        }
    }
}
