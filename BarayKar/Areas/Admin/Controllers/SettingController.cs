using Application.Cqrs.Setting;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController (IMediator mediator): Controller
    {
        private readonly IMediator _mediator=mediator;
        [Route("/Admin/Setting")]
        [HttpGet]
        public async Task<IActionResult> Setting(CancellationToken cancellation=default)
        {
            var pageModel = await _mediator.Send(new GetSettingQuery(), cancellation);
            return View(pageModel.Data);
        }
        [Route("/Admin/Setting")][HttpPost]
        public async Task<IActionResult> Setting(UpdateSettingCommand command
            ,CancellationToken cancellation)
        {
            var result = await _mediator.Send(command, cancellation);
            return RedirectToAction(nameof(Setting));   
        }
    }
}
