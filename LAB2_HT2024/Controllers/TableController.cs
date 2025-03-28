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

        //[HttpPost]
        //public async Task<IActionResult> DeleteTable(TableViewModel tableViewModel)
        //{
        //    var token = HttpContext.Request.Cookies["jwtToken"];

        //    if (string.IsNullOrEmpty(token))
        //    {
        //        TempData["Error"] = "Unauthorized: No token found,";
        //        return RedirectToAction("Index");
        //    }
        //    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


        //    var response_1 = await _client.GetAsync($"{baseUrl}/api/Table/GetTableById/{tableViewModel.Id}");
        //    var json_1 = await response_1.Content.ReadAsStringAsync();
        //    var table = JsonConvert.DeserializeObject<TableViewModel>(json_1);

        //    if (table != null)
        //    {
        //        var json_2 = JsonConvert.SerializeObject(tableViewModel);
        //        var response_2 = await _client.DeleteAsync($"{baseUrl}/api/Table/delete/{tableViewModel.Id}");
        //    }

        //    if (response_2.IsSuccessStatusCode)
        //    {

        //        TempData["Success"] = "Table deleted successfully.";
        //        return RedirectToAction("Index");
        //    }

        //    ModelState.AddModelError("",$"Failed to delete {responsecontent} with status {response.StatusCode}.");
        //    return RedirectToAction("Delete", new { TableId });

        //}

        [HttpPost]
        public async Task<IActionResult> DeleteTable(int TableId)
        {
            var token = HttpContext.Request.Cookies["jwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found,";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            var response = await _client.DeleteAsync($"{baseUrl}/api/Table/delete/{TableId}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = $"Table:{TableId} deleted successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $"Failed to delete {TableId} with status {response.StatusCode}.");
            return View(new { TableId });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTable(int TableId)
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

        [HttpPut]
        public async Task<IActionResult> UpdateTable(TableViewModel tableViewModel)
        {
            var token = HttpContext.Request.Cookies["jwtToken"];

            if (string.IsNullOrEmpty(token))
            {

                TempData["Error"] = "Unauthorized: No token found,";
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
                TempData["Success"] = $"Successfully updated the {responsecontent}";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $"Failed to update {responsecontent} with status {response.StatusCode}.");

            return View(tableViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddTable(TableViewModel tableViewModel)
        {
            var token = HttpContext.Request.Cookies["jwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found,";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(tableViewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{baseUrl}api/Table/add", content);
            var responsecontent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var addedTableJson = JsonConvert.DeserializeObject<TableViewModel>(responsecontent);
                TempData["Success"] = $"Successfully updated the {responsecontent}";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $"Failed to update {responsecontent} with status {response.StatusCode}.");
            return View(tableViewModel);
        }
    }
}