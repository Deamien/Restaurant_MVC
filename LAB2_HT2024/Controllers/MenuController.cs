using LAB2_HT2024.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LAB2_HT2024.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly HttpClient _client;

        private string baseUrl = "https://localhost:7194";

        public MenuController(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync($"{baseUrl}api/Menu");

            var json = await response.Content.ReadAsStringAsync();

            var menuList = JsonConvert.DeserializeObject<List<MenuViewModel>>(json);

            return View(menuList);
        }
    }
}