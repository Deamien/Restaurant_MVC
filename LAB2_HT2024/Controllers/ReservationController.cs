using LAB2_HT2024.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LAB2_HT2024.Controllers
    {
    public class ReservationController : Controller
        {
        private readonly HttpClient _client;

        private string baseUrl = "https://localhost:7194";

        public ReservationController(HttpClient client)
            {
            _client = client;
            }

        public async Task<IActionResult> Index()
            {
            var token = HttpContext.Request.Cookies["jwtToken"];

            if (string.IsNullOrEmpty(token))
                {
                TempData["Error"] = "Unauthorized: No token found,";
                return RedirectToAction("Index");
                }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{baseUrl}api/Reservation");

            var json = await response.Content.ReadAsStringAsync();

            var reservationList = JsonConvert.DeserializeObject<List<ReservationViewModel>>(json);

            return View(reservationList);
            }
        }
    }
