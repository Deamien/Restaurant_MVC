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
            var response = await _client.PostAsJsonAsync($"{baseUrl}/api/Admin/login);'

            if(response.)

        }

        public async Task<IActionResult> Logout();
    }
}
