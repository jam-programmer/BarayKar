using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Component
{
    public class ProvinceInformationComponent(IHomeFactory factory):ViewComponent
    {
        private readonly IHomeFactory _homeFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _homeFactory.GetProvinceInfoAsync();  
            return View("/Views/Shared/ViewComponent/ProvinceInformationSection.cshtml", model);
        }
    }
}
