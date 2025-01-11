using DotNetCoreSqlDb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNetCoreSqlDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> DisplayHtml()
        {
            string htmlContent;
            using (var httpClient = new HttpClient())
            {
                // Replace with your storage container URL
                var response = await httpClient.GetAsync("https://serviceconnector01.blob.core.windows.net/demo/index.html");
                response.EnsureSuccessStatusCode();
                htmlContent = await response.Content.ReadAsStringAsync();
            }

            ViewData["HtmlContent"] = htmlContent;
            return View();
        }
    }
}
