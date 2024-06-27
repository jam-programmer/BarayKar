﻿using Application.Common.Model.CustomModel;
using Application.Cqrs.BusinessProperty;
using BarayKar.Areas.Admin.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarayKar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BusinessPropertyController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]
        ChildPagination pagination, CancellationToken cancellation = default)
        {
            var pageModel = await _mediator.Send(new GetBusinessPropertiesQuery
            { pagination = pagination },
                cancellation);
            ViewBag.Parent = pagination.ParentId;
            return View(pageModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create(Guid ParentId)
        {
            if (ParentId == Guid.Empty)
            {
                return RedirectToAction("Index", "Business");
            }
            ViewBag.Parent = ParentId;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(InsertBusinessPropertyCommand command, CancellationToken cancellation)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command, cancellation);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index), new { parentId = command.BusinessId });
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
            ViewBag.Parent = command.BusinessId;
            return View(command);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id, Guid ParentId,
            CancellationToken cancellation = default)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            var pageModel = await _mediator.Send(new GetBusinessPropertyQuery
            {
                Id = Id
            }, cancellation);
            if (pageModel.IsSuccess)
            {

                return View(pageModel.Data);
            }
            return RedirectToAction(nameof(Index), new { parentId = ParentId });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBusinessPropertyCommand command, CancellationToken cancellation = default)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command, cancellation);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index), new { parentId = command.BusinessId });
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
            var requestModel = await _mediator.Send(new DeleteBusinessPropertyCommand()
            {
                Id = model.Id
            }, cancellation);

            return Ok(requestModel);
        }
    }
}