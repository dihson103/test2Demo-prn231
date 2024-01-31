using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private string baseUrl = "https://localhost:7042/api/DummyDetails";
        private bool isUsedSearch(string url)
        {
            return url.Contains("?$");
        }
        public async Task<IActionResult> Index(String detailName, int masterId)
        {

            if (!string.IsNullOrEmpty(detailName))
            {
                baseUrl += $"?$filter=contains(detailName, '{detailName}')";
            }

            if(masterId > 0)
            {
                if(isUsedSearch(baseUrl))
                {
                    baseUrl += $" and masterId eq {masterId}";
                }
                else
                {
                    baseUrl += $"?$filter=masterId eq {masterId}";
                }
            }

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(baseUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        var successResponse = JsonConvert.DeserializeObject<List<DetailResponseWithMaster>>(responseJson);
                        var detail = successResponse as List<DetailResponseWithMaster>;

                        //var publishers = getPublishers();
                        //ViewData["PubId"] = new SelectList(publishers.Result, "PubId", "PublisherName");

                        return View(detail);
                    }


                    return View(new List<DetailResponseWithMaster>());
                }
            }

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
    }
}