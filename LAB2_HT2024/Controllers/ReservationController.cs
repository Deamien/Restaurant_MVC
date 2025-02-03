using Microsoft.AspNetCore.Mvc;

namespace LAB2_HT2024.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HttpClient _client;
        private string baseUri = "https://localhost:7194";
        public ReservationController(HttpClient client)
        {
            _client = client;
        }
    }
}
