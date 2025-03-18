using LAB2_HT2024.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LAB2_HT2024.Controllers
{
    [Authorize]
    public class TableController : Controller
    {
        private readonly HttpClient _client;

        private string baseUrl = "https://localhost:7194";

        public TableController(HttpClient client)
        {
            _client = client;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Request.Cookies["jwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found,";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{baseUrl}api/Table/all");

            var json = await response.Content.ReadAsStringAsync();

            var tableList = JsonConvert.DeserializeObject<List<TableViewModel>>(json);

            return View(tableList);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int TableId)
        {

            var token = HttpContext.Request.Cookies["jwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found,";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{baseUrl}/api/Table/GetTableById/{TableId}");

            var json = await response.Content.ReadAsStringAsync();

            var table = JsonConvert.DeserializeObject<TableViewModel>(json);



            return View(table);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int TableId)
        {
            var token = HttpContext.Request.Cookies["jwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found,";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"{baseUrl}/api/Table/delete/{TableId}");

            if (!response.IsSuccessStatusCode)
            {

                TempData["Error"] = "Failed to delete the table.";
                return RedirectToAction("Index");
            }


            TempData["Success"] = "Table deleted successfully.";
            return RedirectToAction("Index");
        }


        [HttpPut]
        public async Task<IActionResult> Update(int TableId)
        {
            var token = HttpContext.Request.Cookies["jwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found,";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {

            var token = HttpContext.Request.Cookies["jwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found,";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token); token);

        }
    }
}