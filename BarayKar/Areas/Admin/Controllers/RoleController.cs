using Application.Common.Model;
using Application.Cqrs.Identity.Role;
using BarayKar.Areas.Admin.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Pagination pagination, CancellationToken cancellationToken)
        {
            var pageModel = await _mediator.Send(new GetRolesQuery()
            {
               pagination=pagination
            }, cancellationToken);
            return View(pageModel);
        }


        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create
            (InsertRoleCommand command, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command, cancellationToken);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (result.Message != null)
                {
                    string error = string.Empty;
                    foreach (var item in result.Message!)
                    {
                        error += item;
                    }
                    ViewBag.Alert = error;
                }
            }

            return View(command);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellation = default)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            var pageModel = await _mediator.Send(new GetRoleQuery()
            {
                Id = id
            }, cancellation);
            if (pageModel.IsSuccess)
            {
                return View(pageModel.Data);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRoleCommand command, CancellationToken cancellation)
        {
            if (ModelState.IsValid)
            {

                var result = await _mediator.Send(command, cancellation);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (result.Message != null)
                {
                    string error = string.Empty;
                    foreach (var item in result.Message!)
                    {
                        error += item;
                    }
                    ViewBag.Alert = error;
                }
            }
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] IdInputModel model, CancellationToken cancellation)
        {

            if (model == null)
            {
                return BadRequest();
            }
            var requestModel = await _mediator.Send(new DeleteRoleCommand()
            {
                Id = model.Id
            }, cancellation);

            return Ok(requestModel);
        }

    }
}
