using LAB2_HT2024.Models.MenuViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
           
            var response = await _client.GetAsync($"{baseUrl}api/menu/all");

            var json = await response.Content.ReadAsStringAsync();

            var menuList = JsonConvert.DeserializeObject<List<GetMenuViewModel>>(json);

            return View(menuList);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMenuItem(int MenuItemId)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{baseUrl}api/menu/{MenuItemId}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var MenuItem = JsonConvert.DeserializeObject<GetMenuViewModel>(responseContent);
                
                return View(MenuItem);
            }
            else
            {
                TempData["Error"] = $"Failed to fetch menu item {MenuItemId} for deletion.";
                return RedirectToAction("Index");
            }
        }
       
        [HttpPost]
        public async Task<IActionResult> DeleteMenuItem(GetMenuViewModel getMenuViewModel)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"{baseUrl}api/menu/delete/{getMenuViewModel.MenuItemId}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = $"Successfully deleted dish:{getMenuViewModel.MenuItemId}.";
                return RedirectToAction("Index");
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                TempData["Error"] = $"Failed to delete dish:{getMenuViewModel.MenuItemId}, status:{response.StatusCode} and details:{responseContent}.";
                return RedirectToAction("DeleteMenuItem", new { MenuItemId = getMenuViewModel.MenuItemId });
            }
        }
    }
}