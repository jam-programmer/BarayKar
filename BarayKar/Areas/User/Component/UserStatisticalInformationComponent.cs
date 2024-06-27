using Application.Factories.User;
using Microsoft.AspNetCore.Mvc;
using NLog.Config;

namespace BarayKar.Areas.User.Component
{
    public class UserStatisticalInformationComponent(IUserFactory factory):ViewComponent
    {
        private readonly IUserFactory _userFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {

            var model = await _userFactory.GetUserStatisticalInformationAsync(Id);
            return View("~/Areas/User/Views/Shared/ViewComponent/UserStatisticalInformationSection.cshtml", model);
        }
    }
}
