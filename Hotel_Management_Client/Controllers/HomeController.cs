using Hotel_Management_Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;


namespace Hotel_Management_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private readonly HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Fetch the list of floors from the API
            HttpResponseMessage floorsResponse = await _client.GetAsync("https://localhost:7083/api/Floors");

            if (floorsResponse.IsSuccessStatusCode)
            {
                var floorsJson = await floorsResponse.Content.ReadAsStringAsync();
                var floors = JsonConvert.DeserializeObject<List<HomeViewModel.FloorViewModel>>(floorsJson);

                // Fetch the list of rooms for each floor
                foreach (var floor in floors)
                {
                    HttpResponseMessage roomsResponse = await _client.GetAsync($"https://localhost:7083/api/Rooms/ByFloor/{floor.FloorId}");
                    if (roomsResponse.IsSuccessStatusCode)
                    {
                        var roomsJson = await roomsResponse.Content.ReadAsStringAsync();
                        floor.Rooms = JsonConvert.DeserializeObject<List<HomeViewModel.RoomViewModel>>(roomsJson);
                    }
                }

                return View(floors);
            }

            // Handle the case where the API request was not successful
            return View("Error");

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