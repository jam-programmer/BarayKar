using Application.Factories.User;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Areas.User.Component
{
    public class UserInformationComponent(IUserFactory factory):ViewComponent
    {
        private readonly IUserFactory _userFactory = factory;
        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {

            var model = await _userFactory.GetUserInformationAsync(Id);
            return View("~/Areas/User/Views/Shared/ViewComponent/UserInformationSection.cshtml", model);
        }
    }
}
