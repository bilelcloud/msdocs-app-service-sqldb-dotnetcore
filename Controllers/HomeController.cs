using DotNetCoreSqlDb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DotNetCoreSqlDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
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
            // string url = _configuration["StorageSettings:HtmlUrl"];
            string endpoint = Environment.GetEnvironmentVariable("AZURE_STORAGEBLOB_RESOURCEENDPOINT") ?? _configuration["AZURE_STORAGEBLOB_RESOURCEENDPOINT"];
            string containerName = Environment.GetEnvironmentVariable("ContainerName") ?? _configuration["ContainerName"];
            string fileName = Environment.GetEnvironmentVariable("FileName") ?? _configuration["FileName"];
            string url = $"{endpoint}{containerName}/{fileName}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                htmlContent = await response.Content.ReadAsStringAsync();
            }

            ViewData["HtmlContent"] = htmlContent;
            return View();
        }
    }
}
