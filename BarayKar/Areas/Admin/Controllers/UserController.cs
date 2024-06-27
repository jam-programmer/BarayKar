using Application.Common.Model;
using Application.Cqrs.Identity.Role;
using Application.Cqrs.Identity.User;
using BarayKar.Areas.Admin.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarayKar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;



        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Pagination pagination,
            CancellationToken cancellationToken)
        {
            var pageModel = await _mediator.Send(new GetUsersQuery()
            {
               pagination=pagination
            }, cancellationToken);
            return View(pageModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await Roles();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(InsertUserCommand command, CancellationToken cancellation)
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
            await Roles(command.Roles);
            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id, CancellationToken cancellation = default)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            var pageModel = await _mediator.Send(new GetUserQuery()
            {
                Id = Id
            }, cancellation);

            if (pageModel.IsSuccess)
            {
                await Roles();
                return View(pageModel.Data);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserCommand command, CancellationToken cancellation)
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
            await Roles();
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] IdInputModel model,
            CancellationToken cancellation)
        {

            if (model == null)
            {
                return BadRequest();
            }
            var requestModel = await _mediator.Send(new DeleteUserCommand()
            {
                Id = model.Id
            }, cancellation);

            return Ok(requestModel);
        }


        public async Task Roles(List<string>? selected = null)
        {
            var roles = await _mediator.Send(new GetRoleItemsQuery());
            if (selected == null)
            {
                ViewBag.Roles = new SelectList(roles, "Id", "Title");
            }
            else
            {
                ViewBag.Roles = new SelectList(roles, "Id", "Title", selected);

            }
        }
    }
}
