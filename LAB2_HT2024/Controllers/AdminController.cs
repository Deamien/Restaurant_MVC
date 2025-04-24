using LAB2_HT2024.Models;
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
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginViewModel)
        {
            var response = await _client.PostAsJsonAsync($"{baseUrl}api/admin/login", adminLoginViewModel);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = $"Successfully logged in as admin:{admin.MenuItemId}.";
                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> Logout();
    }
}
