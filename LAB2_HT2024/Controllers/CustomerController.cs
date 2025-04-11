﻿ using LAB2_HT2024.Models.CustomerViewModels;
using LAB2_HT2024.Models.MenuViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LAB2_HT2024.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly HttpClient _client;

        private readonly string baseUrl = "https://localhost:7194";

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
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }

            //Lägger till token in i http request header
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            //Hämtar alla Customers från API endpoint
            var response = await _client.GetAsync($"{baseUrl}api/Customer/all");

            //Läser av json som en string
            var json = await response.Content.ReadAsStringAsync();

            //json blir deserialized för att bli till en object
            var customerList = JsonConvert.DeserializeObject<List<GetCustomerViewModel>>(json);

            return View(customerList);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCustomer(int CustomerId)
        {
            var token = HttpContext.Request.Cookies["´JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            
            var response = await _client.GetAsync($"{baseUrl}/api/Customer/add");
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Customer = JsonConvert.DeserializeObject<GetCustomerViewModel>(responseContent);
                
                return View(Customer);
            }
            else
            {
                TempData["Error"] = $"Failed to fetch customer with id:{CustomerId} for deletion.";
                
                return RedirectToAction($"Index");
            }
                
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(GetCustomerViewModel getCustomerViewModel)
        {
            var token = HttpContext.Request.Cookies["´JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"{baseUrl}/api/Customer/add");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Customer = JsonConvert.DeserializeObject<GetCustomerViewModel>(responseContent);

                TempData["Success"] = $"Successfully deleted customer:{getCustomerViewModel.CustomerId}.";
                return RedirectToAction ("");
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                TempData["Error"] = $"Failed to delete customer with id:{getCustomerViewModel.CustomerId}, status:{response.StatusCode} and details:{responseContent}. for deletion.";
                return RedirectToAction($"Index", new { CustomerId = getCustomerViewModel.CustomerId });
            }
        } 
    }
}
