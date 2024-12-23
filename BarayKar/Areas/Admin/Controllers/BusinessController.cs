﻿using Application.Common.Model;
using Application.Cqrs.Business;
using Application.Cqrs.Category;
using Application.Cqrs.City;
using Application.Cqrs.Identity.User;
using Application.Cqrs.Province;
using BarayKar.Areas.Admin.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarayKar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BusinessController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Pagination pagination, CancellationToken cancellation = default)
        {
            var pageModel = await _mediator.Send(new GetBusinesiesQuery
            { pagination = pagination },
                cancellation);
            return View(pageModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await Categories();
            await Users();
            await Province();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InsertBusinessCommand command, CancellationToken cancellation)
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
            await Categories();
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
            var pageModel = await _mediator.Send(new GetBusinessQuery
            {
                Id = Id
            }, cancellation);
            if (pageModel.IsSuccess)
            {
                await Categories(((UpdateBusinessCommand)pageModel.Data!).CategoryId.ToString());
                await Users(((UpdateBusinessCommand)pageModel.Data!).UserId.ToString());
                await Province(((UpdateBusinessCommand)pageModel.Data!).ProvinceId.ToString());
                await City(((UpdateBusinessCommand)pageModel.Data!).CityId.ToString());
                return View(pageModel.Data);
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBusinessCommand command, CancellationToken cancellation = default)
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
            await Categories(command.CategoryId.ToString());
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
            var requestModel = await _mediator.Send(new DeleteBusinessCommand()
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
        public async Task<IActionResult> Cities([FromBody]IdInputModel? parent,CancellationToken cancellation=default)
        {
            var cities = await _mediator.Send(new GetCityItemsByIdQuery()
            {
                ParentId=parent!.Id
            },cancellation);
            return Ok(cities);
        }

    }
}
