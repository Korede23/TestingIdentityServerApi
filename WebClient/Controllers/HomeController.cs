using Client.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebClient.Models;
using WebClient.Service;
using static System.Net.WebRequestMethods;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        public ITokenService _tokenService { get; }

        public HomeController(ILogger<HomeController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Room()
        {
            var data = new List<RoomModel>();
            var token = await _tokenService.GetToken("myApi.read");
            using (var client = new HttpClient())
            {
                client.SetBearerToken(token.AccessToken);
                var result = await client.GetAsync("https://localhost:7038/roomcontroller");
                if (result.IsSuccessStatusCode)
                {
                    var model = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<RoomModel>>(model);
                    return View(data);
                }
                else
                {
                    throw new Exception("Failed to get Data from API");
                }
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
