using LAB2_HT2024.Models.ReservationViewModels;
using LAB2_HT2024.Models.TableViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Text;

namespace LAB2_HT2024.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HttpClient _client;

        private readonly string baseUrl = "https://localhost:7194/";

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

            var response = await _client.GetAsync($"{baseUrl}api/reservation/all");

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

            var response = await _client.GetAsync($"{baseUrl}api/reservation/{ReservationId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var reservation = JsonConvert.DeserializeObject<UpdateReservationViewModel>(json);
                
                TempData["Success"] = $"Successfully loaded reservation:{reservation.ReservationId}";
                return View(reservation);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                var reservation = JsonConvert.DeserializeObject<UpdateReservationViewModel>(json);
                
                TempData["Error"] = $"Failed to load reservation {reservation.ReservationId} for an update.";
                return RedirectToAction("Index");
            }
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
            
            var json = JsonConvert.SerializeObject(updateReservationViewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{baseUrl}api/reservation/update/{updateReservationViewModel}", content);
            
            var responsecontent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                var updatedReservationJson = JsonConvert.DeserializeObject<UpdateReservationViewModel>(responsecontent);
                TempData["Success"] = $"Successfully updated ReservationId: {updatedReservationJson.ReservationId}";
                return RedirectToAction("Index");
            }
            else
            {
                var updatedReservationJson = JsonConvert.DeserializeObject<UpdateReservationViewModel>(responsecontent);
                TempData["Error"] = $"Failed to update reservation with id:{updatedReservationJson.ReservationId}, status:{response.StatusCode} and details:{responsecontent};";
                return RedirectToAction("UpdateReservation");
            }
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
