using Microsoft.AspNetCore.Mvc;

namespace LAB2_HT2024.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _client;
        
        private string baseUri = "https://localhost:7194";
        
        public CustomerController(HttpClient client)
        {
            _client = client;
        }



        public async Task<ActionResult> Index() 
        { 
        

            return View();
        }
    }
}
