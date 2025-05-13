using LAB2_HT2024.Models.TableViewModels;
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

        private readonly string baseUrl = "https://localhost:7194/";

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

            var response = await _client.GetAsync($"{baseUrl}api/table/all");

            var json = await response.Content.ReadAsStringAsync();

            var tableList = JsonConvert.DeserializeObject<List<GetTableViewModel>>(json);

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

            var response = await _client.GetAsync($"{baseUrl}api/table/{TableId}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var table = JsonConvert.DeserializeObject<GetTableViewModel>(responseContent);
                return View(table);
            }
            else
            {
                TempData["Error"] = $"Failed to fetch table with ID {TableId}.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTable(GetTableViewModel getTableViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("DeleteTable");
            }

            var token = HttpContext.Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"{baseUrl}api/table/delete/{getTableViewModel.TableId}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = $"Successfully deleted Table:{getTableViewModel.TableId}.";
                return RedirectToAction("Index");
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                TempData["Error"] = $"Failed to delete table with id:{getTableViewModel.TableId}, status:{response.StatusCode} and details:{responseContent}.";
                return RedirectToAction($"Index", new { TableId = getTableViewModel.TableId });
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

            var response = await _client.GetAsync($"{baseUrl}api/table/{TableId}");


            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var table = JsonConvert.DeserializeObject<UpdateTableViewModel>(json);

                TempData["Success"] = $"Successfully loaded table:{TableId}.";
                return View(table);
            }
            else
            {
                TempData["Error"] = $"Failed to load table {TableId} for an update.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTable(UpdateTableViewModel updateTableViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(updateTableViewModel);
            }

            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }

            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var json = JsonConvert.SerializeObject(updateTableViewModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"{baseUrl}api/table/update/{updateTableViewModel.TableId}", content);

                var responsecontent = await response.Content.ReadAsStringAsync();
                var updatedTableJson = JsonConvert.DeserializeObject<UpdateTableViewModel>(responsecontent);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = $"Successfully updated TableId: {updatedTableJson.TableId}";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = $"Failed to update table with id:{updatedTableJson.TableId}, status:{response.StatusCode} and details:{responsecontent}.";
                    return RedirectToAction("UpdateTable", new { TableId = updateTableViewModel.TableId });
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Network error: {ex.Message}");
                return View(updateTableViewModel);
            }
            catch (JsonException ex)
            {
                ModelState.AddModelError("", $"Error processing response: {ex.Message}");
                return View(updateTableViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTable(AddTableViewModel addTableViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addTableViewModel);
            }

            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }

            try
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var json = JsonConvert.SerializeObject(addTableViewModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{baseUrl}api/table/add", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var addedTableJson = JsonConvert.DeserializeObject<GetTableViewModel>(responseContent);

                if (response.IsSuccessStatusCode)
                {

                    TempData["Success"] = $"Successfully added TableId:{addedTableJson.TableId}";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", $"Failed to add Table:{addedTableJson.TableId} with status:{response.StatusCode} and details:{responseContent}.");
                return View(addTableViewModel);
            }

            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Network error: {ex.Message}");
                return View(addTableViewModel);
            }
            catch (JsonException ex)
            {
                ModelState.AddModelError("", $"Error processing response: {ex.Message}");
                return View(addTableViewModel);
            }
        }
    }
}