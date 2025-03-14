using LAB2_HT2024.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LAB2_HT2024.Controllers
{
    [Authorize]
    public class TableController : Controller
    {
        private readonly HttpClient _client;

        private string baseUrl = "https://localhost:7194";

        public TableController(HttpClient client)
        {
            _client = client;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync($"{baseUrl}api/Table/all");

            var token = HttpContext.Request.Cookies["jwtToken"];
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var json = await response.Content.ReadAsStringAsync();

            var tableList = JsonConvert.DeserializeObject<List<TableViewModel>>(json);

            return View(tableList);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int TableId)
        {

            var token = HttpContext.Request.Cookies["jwtToken"];
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{baseUrl}/api/Table/GetTableById/{TableId}");

            var json = await response.Content.ReadAsStringAsync();

            var table = JsonConvert.DeserializeObject<TableViewModel>(json);



            return View(table);

            [HttpPost]
            public async Task<IActionResult> Delete(int TableId)
            {



            }

            [HttpPut]
            public async Task<IActionResult> Update(int TableId)
            {


            }

            [HttpPost]
            public async Task<IActionResult> Add()
            {

                var token = HttpContext.Request.Cookies["jwtToken"];
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            }




        }
    }
