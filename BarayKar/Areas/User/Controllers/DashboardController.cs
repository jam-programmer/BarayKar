using Application.Common.Model;
using Application.Common.Record.UserPanel;
using Application.Cqrs.Business;
using Application.Cqrs.Category;
using Application.Cqrs.City;
using Application.Cqrs.Employment;
using Application.Cqrs.Identity.User;
using Application.Cqrs.Province;
using Application.Factories.User;
using BarayKar.Areas.Admin.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BarayKar.Areas.User.Controllers
{
    [Area("User")]
    public class DashboardController(IUserFactory factory,IMediator mediator) : Controller
    {
        private readonly IMediator _mediator=mediator;
        private readonly IUserFactory _userFactory = factory;
        public IActionResult Index()
        {
            ViewBag.UserId = UserId();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(CancellationToken cancellationToken)
        {
            var id = UserId();
            var pageModel = await _userFactory.GetUserInformationProfileAsync(id, cancellationToken);
            return View(pageModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserInformationRecord record,CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result=await _userFactory.UpdateUserInformationProfileAsync(record, cancellationToken);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Alert = result.Message!.FirstOrDefault();
            }
            return View(record);
        }


        public async Task<IActionResult> UserBusinesses(CancellationToken cancellation=default)
        {
            var pageModel=await _userFactory.GetUserBusinessesAsync(UserId(),cancellation); 
            return View(pageModel);
        }
        [HttpGet]
        public async Task<IActionResult>AddBusiness()
        {
            await Categories();
            await Province();
            ViewBag.UserId=UserId();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBusiness(AddBusinessRecord record,CancellationToken cancellation=default)
        {
            if(ModelState.IsValid)
            {
                var result = await _userFactory.AddBusinessAsync(record, cancellation);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(UserBusinesses));
                }
                ViewBag.Alert=result.Message!.FirstOrDefault();
            }

            await Categories();
            await Province();
            ViewBag.UserId = UserId();
            return View(record);
        }


        [HttpGet]
        public async Task<IActionResult>EditBusiness(Guid Id,CancellationToken cancellation=default)
        {
            var pageModel=await _userFactory.GetUserBusinessByIdAsync(Id,cancellation);
            await Categories(pageModel.CategoryId.ToString());
            await Province(pageModel.ProvinceId.ToString());
            await City(pageModel.CityId.ToString());
            return View(pageModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditBusiness(UpdateBusinessRecord record,CancellationToken cancellation=default)
        {
            if (ModelState.IsValid)
            {
                 var result = await _userFactory.UpdateUserBusinessAsync(record, cancellation);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Alert = result.Message!.FirstOrDefault();
            }

            await Categories();
            await Province();
            await City();
        
            return View(record);
        }

        [HttpGet]
        public async Task<IActionResult> UserEmploymentAdvertisements([FromQuery] Pagination pagination,CancellationToken cancellation)
        {
            var userId =  UserId();
            var pageModel = await _userFactory.GetUserEmploymentAdvertisementsAsync(new UserPaginationRecord
            {
                pagination=pagination,
                UserId=userId
            }, cancellation);
            return View(pageModel);
        }






        [HttpGet]
        public async Task<IActionResult> AddEmployment()
        {
            ViewBag.UserId=UserId();
            await Businesses();
            await Province();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployment(AddEmploymentRecord record,
           CancellationToken cancellation=default)
        {
            if (ModelState.IsValid)
            {
                var result = await _userFactory.AddEmploymentAsync(record, cancellation);
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
            ViewBag.UserId = UserId();
            await Province();
            return View(record);
        }



        [HttpGet]
        public async Task<IActionResult> EditEmployment(Guid Id,CancellationToken cancellation)
        {
            var pageModel = await _userFactory.GetUserEmploymentByIdAsync(Id, cancellation);
            if(!pageModel.IsSuccess)
            {
                return RedirectToAction(nameof(UserEmploymentAdvertisements));
            }
            await Businesses(((UpdateEmploymentRecord)pageModel.Data!).BusinessId.ToString());
            await Province(((UpdateEmploymentRecord)pageModel.Data!).ProvinceId.ToString());
            await City(((UpdateEmploymentRecord)pageModel.Data!).CityId.ToString());
            return View(pageModel.Data);
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployment(UpdateEmploymentRecord record,CancellationToken cancellation=default)
        {
            if (ModelState.IsValid)
            {
                var result = await _userFactory.UpdateUserEmploymentAsync(record, cancellation);
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
            await Businesses(record.BusinessId.ToString());
            await Province(record.ProvinceId.ToString());
            await City(record.CityId.ToString());
            return View(record);
        }



        [HttpGet]
        public async Task<IActionResult> MyResume()
        {
            var pageModel = await _userFactory.GetUserResumeAsync(UserId());
            return View(pageModel);
        }
        [HttpPost]
        public async Task<IActionResult> MyResume(MyResumeRecord record,
            CancellationToken cancellation=default)
        {
            var result = await _userFactory.UpdateMyResumeAsync(record, cancellation);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(MyResume));
            }
           ViewBag.Alert=result.Message;
            return View(record);
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
        public async Task<IActionResult> Cities([FromBody] IdInputModel? parent, CancellationToken cancellation = default)
        {
            var cities = await _mediator.Send(new GetCityItemsByIdQuery()
            {
                ParentId = parent!.Id
            }, cancellation);
            return Ok(cities);
        }



        public Guid UserId()
        {
            return Guid.Parse(User.FindFirstValue("Id")!);
        }
    }
}
