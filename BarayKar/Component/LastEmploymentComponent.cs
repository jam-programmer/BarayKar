using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Component
{
    public class LastEmploymentComponent (IHomeFactory factory):ViewComponent
    {
        private readonly IHomeFactory _homeFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _homeFactory.GetLastEmploymentAsync();
            return View("/Views/Shared/ViewComponent/LastEmploymentSection.cshtml", model);
        }
    }
}
