using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Areas.User.Component
{
    public class UserDashboardMenuComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

           
            return View("~/Areas/User/Views/Shared/ViewComponent/UserDashboardMenuSection.cshtml");
        }
    }
}
