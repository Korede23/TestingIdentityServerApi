using Client.Service;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
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
            return Ok();
        }

        public IActionResult Privacy()
        {
            return Ok();
        }

        public async Task<IActionResult> Weather()
        {
            var data = new List<WeatherModel>();
            var token = await _tokenService.GetToken("myApi.read");
            using (var client = new HttpClient())
            {
                client.SetBearerToken(token.AccessToken);
                var result = await client.GetAsync("https://localhost:7112/weatherforecast");
                if (result.IsSuccessStatusCode)
                {
                    var model = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<WeatherModel>>(model);
                    return Ok(data);
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
            return Ok(new  { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
