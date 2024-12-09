using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Component
{
    public class ContactUsInformationComponent(IHomeFactory factory) : ViewComponent
    {
        private readonly IHomeFactory _homeFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _homeFactory.GetContactUsInformationAsync();
            return View("/Views/Shared/ViewComponent/ContactUsInformationSection.cshtml", 
                model);
        }
    }
}
