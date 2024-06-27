using Application.Common.Model;
using Application.Cqrs.Business;
using Application.Cqrs.City;
using Application.Cqrs.Employment;
using Application.Cqrs.Identity.User;
using Application.Cqrs.Province;
using BarayKar.Areas.Admin.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarayKar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmploymentController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Pagination pagination,
            CancellationToken cancellation = default)
        {
            var pageModel = await _mediator.Send(new GetEmploymentsQuery
            { pagination = pagination },
                cancellation);
            return View(pageModel);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await Businesses();
            await Users();
            await Province();
            return View();
        }





        [HttpPost]
        public async Task<IActionResult> Create(InsertEmploymentCommand command,
            CancellationToken cancellation)
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
            await Businesses();
            await Users();
            await Province();
            return View(command);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id, CancellationToken cancellation = default)
        {
            if (Id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            var pageModel = await _mediator.Send(new GetEmploymentQuery
            {
                Id = Id
            }, cancellation);
            if (pageModel.IsSuccess)
            {
                await Businesses(((UpdateEmploymentCommand)pageModel.Data!).BusinessId.ToString());
                await Users(((UpdateEmploymentCommand)pageModel.Data!).UserId.ToString());
                await Province(((UpdateEmploymentCommand)pageModel.Data!).ProvinceId.ToString());
                await City(((UpdateEmploymentCommand)pageModel.Data!).CityId.ToString());
                return View(pageModel.Data);
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> Edit(UpdateEmploymentCommand command, CancellationToken cancellation = default)
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
            await Businesses(command.BusinessId.ToString());
            await Users(command.UserId.ToString());
            await Province(command.ProvinceId.ToString());
            await City(command.CityId.ToString());
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
            var requestModel = await _mediator.Send(new DeleteEmploymentCommand()
            {
                Id = model.Id
            }, cancellation);

            return Ok(requestModel);
        }













        public async Task Businesses(string? selected = null)
        {
            var businesses = await _mediator.Send(new GetBusinessItemsQuery());
            if (selected == null)
            {
                ViewBag.Businesses = new SelectList(businesses, "Id", "Title");
            }
            else
            {
                ViewBag.Businesses = new SelectList(businesses, "Id", "Title", selected);

            }
        }


        public async Task Users(string? selected = null)
        {
            var users = await _mediator.Send(new GetUserItemsQuery());
            if (selected == null)
            {
                ViewBag.Users = new SelectList(users, "Id", "Title");
            }
            else
            {
                ViewBag.Users = new SelectList(users, "Id", "Title", selected);

            }
        }
        public async Task Province(string? selected = null)
        {
            var province = await _mediator.Send(new GetProvinceItemsQuery());
            if (selected == null)
            {
                ViewBag.Province = new SelectList(province, "Id", "Title");
            }
            else
            {
                ViewBag.Province = new SelectList(province, "Id", "Title", selected);

            }
        }
        public async Task City(string? selected = null)
        {
            var city = await _mediator.Send(new GetCityItemsQuery());
            if (selected == null)
            {
                ViewBag.City = new SelectList(city, "Id", "Title");
            }
            else
            {
                ViewBag.City = new SelectList(city, "Id", "Title", selected);

            }
        }
        public async Task<IActionResult> Cities([FromBody] IdInputModel? parent,
            CancellationToken cancellation = default)
        {
            var cities = await _mediator.Send(new GetCityItemsByIdQuery()
            {
                ParentId = parent!.Id
            }, cancellation);
            return Ok(cities);
        }

    }
}
