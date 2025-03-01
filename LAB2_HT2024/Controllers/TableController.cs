using LAB2_HT2024.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LAB2_HT2024.Controllers
{
    public class TableController : Controller
    {
        private readonly HttpClient _client;

        private string baseUri = "https://localhost:7194";

        public TableController(HttpClient client)
        {
            _client = client;
        }
    
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync($"{baseUri}api/Table");

            var json = await response.Content.ReadAsStringAsync();

            var tableList = JsonConvert.DeserializeObject<List<TableViewModel>>(json);

            return View(tableList);
        }
    }
}
