using LAB2_HT2024.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LAB2_HT2024.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly HttpClient _client;

        private string baseUrl = "https://localhost:7194";

        public CustomerController(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            //Hämtar token från cookies
            var token = HttpContext.Request.Cookies["jwtToken"];

            //Producerar en temporär error message om token är tom.
            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found,";
                return RedirectToAction("Index");
            }

            //Lägger till token in i http request header
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            //Hämtar alla Customers från API endpoint
            var response = await _client.GetAsync($"{baseUrl}api/Customer");

            //Läser av json som en string
            var json = await response.Content.ReadAsStringAsync();

            //json blir deserialized för att bli till en object
            var customerList = JsonConvert.DeserializeObject<List<CustomerViewModel>>(json);

            return View(customerList);
        }
    }
}
