using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Component
{
    public class CategoryInformationComponent(IHomeFactory factory) : ViewComponent
    {
        private readonly IHomeFactory _homeFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _homeFactory.GetCategoriesAsync();
            return View("/Views/Shared/ViewComponent/CategoryInformationSection.cshtml", model);
        }
    }
}
