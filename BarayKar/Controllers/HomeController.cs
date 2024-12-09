using Application.Common.Record.Home;
using Application.Factories.Home;
using BarayKar.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;
using NReco.PdfGenerator;
namespace BarayKar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeFactory _homeFactory;
        public HomeController(ILogger<HomeController> logger, IHomeFactory homeFactory)
        {
            _homeFactory = homeFactory;
            _logger = logger;
        }

        public IActionResult Index()
        {


            return View();
        }
        public async Task<IActionResult> Law()
        {
            var pageModel=await _homeFactory.GetLawAsync(); 
            return View(pageModel);
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        public async Task<IActionResult> AboutUs()
        {
            var pageModel = await _homeFactory.GetAboutAsync();
            return View(pageModel);
        }
       
        public async Task<IActionResult> SendContactMessage
            ([FromBody] AddContactRecord record, CancellationToken cancellation = default)
        {
            var result = await _homeFactory.AddContactMessageAsync(record, cancellation);
            if (result.IsSuccess is true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
