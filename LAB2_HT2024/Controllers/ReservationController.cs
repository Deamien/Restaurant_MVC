using LAB2_HT2024.Models.ReservationViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LAB2_HT2024.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HttpClient _client;

        private readonly string baseUrl = "https://localhost:7194";

        public ReservationController(HttpClient client)
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

            var response = await _client.GetAsync($"{baseUrl}api/Reservation/all");

            var json = await response.Content.ReadAsStringAsync();

            var reservationList = JsonConvert.DeserializeObject<List<GetReservationViewModel>>(json);

            return View(reservationList);
        }

        public async Task<IActionResult> DeleteReservation(int ReservationId)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        }

        public async Task<IActionResult> DeleteReservation(GetReservationViewModel getReservationViewModel)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        }


        [HttpGet]
        public async Task<IActionResult> UpdateReservation(int ReservationId)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservation(UpdateReservationViewModel updateReservationViewModel)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.
        }

        public async Task<IActionResult> AddReservation(AddReservationViewModel addReservationViewModel)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Unauthorized: No token found.";
                return RedirectToAction("Index");
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        }

    }
}
