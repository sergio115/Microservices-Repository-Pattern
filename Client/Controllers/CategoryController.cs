using Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class CategoryController : Controller
    {
        HttpClientHandler _httpClientHandler = new HttpClientHandler();

        public CategoryController()
        {
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        Category category = new Category();
        List<Category> categories = new List<Category>();

        public async Task<ActionResult> Index()
        {
            categories = new List<Category>();

            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44323/api/categories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
                }
            }

            return View(categories);
        }

        public async Task<ActionResult> GetById(int categoryID)
        {
            category = new Category();

            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                using (var response = await httpClient.GetAsync($"https://localhost:44323/api/categories/{categoryID}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    category = JsonConvert.DeserializeObject<Category>(apiResponse);
                }
            }

            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Category category)
        {
            category = new Category();

            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"https://localhost:44323/api/categories", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    category = JsonConvert.DeserializeObject<Category>(apiResponse);
                }
            }

            return View(category);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int categoryID)
        {
            string message = "";

            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:44323/api/products/{categoryID}"))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }

            return View();
        }
    }
}
