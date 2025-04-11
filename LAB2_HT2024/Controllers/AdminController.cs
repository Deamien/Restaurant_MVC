using Microsoft.AspNetCore.Mvc;

namespace LAB2_HT2024.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _client;

        private readonly string baseUrl = "https://localhost:7194";

        public AdminController(HttpClient client)
        {
            _client = client;
        }
        public async Task<IActionResult> Login()
        {
            var response = await _client.PostAsJsonAsync($"{baseUrl}api/admin/login");

            if(response.IsSuccessStatusCode)
            {
                TempData["Success"] = $"Successfully deleted dish:{a dmin.MenuItemId}.";
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult> Logout();
    }
}
