using Application.Common.Model;
using Application.Cqrs.Category;
using Application.Cqrs.Province;
using BarayKar.Areas.Admin.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProvinceController(IMediator mediator) : Controller
    {

        private readonly IMediator _mediator = mediator;
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]
        Pagination pagination, CancellationToken cancellation = default)
        {
            var pageModel = await _mediator.Send(new GetProvincesQuery
            { pagination = pagination },
                cancellation);
            return View(pageModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(InsertProvinceCommand command, CancellationToken cancellation)
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



        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id, CancellationToken cancellation = default)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            var pageModel = await _mediator.Send(new GetProvinceQuery
            {
                Id = Id
            }, cancellation);
            if (pageModel.IsSuccess)
            {
                
                return View(pageModel.Data);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProvinceCommand command, CancellationToken cancellation = default)
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
        public async Task<IActionResult> Delete([FromBody] IdInputModel model,
          CancellationToken cancellation)
        {

            if (model == null)
            {
                return BadRequest();
            }
            var requestModel = await _mediator.Send(new DeleteProvinceCommand()
            {
                Id = model.Id
            }, cancellation);

            return Ok(requestModel);
        }

    }
}
