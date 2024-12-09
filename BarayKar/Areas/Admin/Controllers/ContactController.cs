using Application.Common.Model;
using Application.Cqrs.Business;
using Application.Cqrs.Contact;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Pagination pagination, 
            CancellationToken cancellation = default)
        {
            var pageModel = await _mediator.Send(new GetContactsQuery
            { pagination = pagination },
                cancellation);
            return View(pageModel);
        }

    }
}
