using Application.Factories.Home;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Component
{
    public class ParentCategoryComponentl (IHomeFactory factory): ViewComponent
    {
        private readonly IHomeFactory _homeFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _homeFactory.GetParentCategoryAsync();
            return View("/Views/Shared/ViewComponent/ParentCategorySection.cshtml", model);
        }
    }
}
