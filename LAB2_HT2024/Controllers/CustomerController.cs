using LAB2_HT2024.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LAB2_HT2024.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _client;
        
        private string baseUri = "https://localhost:7194";
        
        public CustomerController(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            //Hämtar alla Customers från API endpoint
            var response = await _client.GetAsync($"{baseUri}api/Customer");

            //Läser av json som en string
            var json = await response.Content.ReadAsStringAsync();
            //json blir deserialized för att bli till en object
            var customerList = JsonConvert.DeserializeObject<List<CustomerViewModel>>(json);

            return View(customerList);
        }
    }
}
