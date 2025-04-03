using LAB2_HT2024.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LAB2_HT2024.Controllers
{
    [Authorize]
    public class TableController : Controller
    {
        private readonly HttpClient _client;

        private readonly string baseUrl = "https://localhost:7194";

        public TableController(HttpClient client)
        {
            _client = client;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{baseUrl}api/Table/all");

            var json = await response.Content.ReadAsStringAsync();

            var tableList = JsonConvert.DeserializeObject<List<TableViewModel>>(json);

            return View(tableList);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTable(int TableId)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{baseUrl}/api/Table/{TableId}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = $"Failed to fetch table with ID {TableId}.";
                return RedirectToAction("Index");
            }
            var json = await response.Content.ReadAsStringAsync();
            var table = JsonConvert.DeserializeObject<TableViewModel>(json);
            return View("DeleteTable", table);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTable(TableViewModel tableViewModel)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"{baseUrl}/api/Table/delete/{tableViewModel.TableId}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = $"Successfully deleted Table:{tableViewModel.TableId}.";
                return RedirectToAction("Index");
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Failed to delete Table:{tableViewModel.TableId} with status:{response.StatusCode} and details:{responseContent}.");
                return View("DeleteTable", tableViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTable(int TableId)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{baseUrl}/api/Table/{TableId}");
            var json = await response.Content.ReadAsStringAsync();
            var table = JsonConvert.DeserializeObject<TableViewModel>(json);

            return View(table);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTable(TableViewModel tableViewModel)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {

                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");

            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(tableViewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{baseUrl}api/Table/update/{tableViewModel}", content);
            var responsecontent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var updatedTableJson = JsonConvert.DeserializeObject<TableViewModel>(responsecontent);
                TempData["Success"] = $"Successfully updated TableId: {updatedTableJson.TableId }";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $"Failed to update {responsecontent} with status {response.StatusCode}.");

            return View(tableViewModel);
        }

        
        [HttpPost]
        public async Task<IActionResult> AddTable(TableViewModel tableViewModel)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(tableViewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{baseUrl}api/Table/add", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var addedTableJson = JsonConvert.DeserializeObject<TableViewModel>(responseContent);

            if (response.IsSuccessStatusCode)
            {
                
                TempData["Success"] = $"Successfully added TableId:{addedTableJson.TableId}";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $"Failed to add TableID:{addedTableJson.TableId} with status:{response.StatusCode} and details:{responseContent}.");
            return View(tableViewModel);
        }
    }
}