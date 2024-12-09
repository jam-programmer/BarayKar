using Application.Common.Model;
using Application.Cqrs.Category;
using BarayKar.Areas.Admin.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarayKar.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController (IMediator mediator): Controller
    {
        private readonly IMediator _mediator=mediator;
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Pagination pagination,CancellationToken cancellation=default)
        {
            var pageModel =await _mediator.Send(new GetCategoriesQuery { pagination = pagination },
                cancellation);
            return View(pageModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await Categories();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(InsertCategoryCommand command, CancellationToken cancellation)
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
            await Categories(command.ParentCategoryId.ToString());
            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id,CancellationToken cancellation=default)
        {
            if(Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            var pageModel = await _mediator.Send(new GetCategoryQuery
            {
                Id = Id
            },cancellation);
            if (pageModel.IsSuccess)
            {
                await Categories(((UpdateCategoryCommand)pageModel.Data!).ParentCategoryId.ToString());
                return View(pageModel.Data);
            }
            return RedirectToAction(nameof(Index));
        }







        [HttpPost]
        public async Task<IActionResult>Edit(UpdateCategoryCommand command,CancellationToken cancellation=default)
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
            await Categories(command.ParentCategoryId.ToString());
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
            var requestModel = await _mediator.Send(new DeleteCategoryCommand()
            {
                Id = model.Id
            }, cancellation);

            return Ok(requestModel);
        }




        public async Task Categories(string? selected = null)
        {
            var categories = await _mediator.Send(new GetCategoryItemsQuery());
            if (selected == null)
            {
                ViewBag.Categories = new SelectList(categories, "Id", "Title");
            }
            else
            {
                ViewBag.Categories = new SelectList(categories, "Id", "Title", selected);

            }
        }
    }
}
