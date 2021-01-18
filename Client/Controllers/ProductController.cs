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
    public class ProductController : Controller
    {
        HttpClientHandler _httpClientHandler = new HttpClientHandler();

        public ProductController()
        {
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        Product product = new Product();
        List<Product> products = new List<Product>();

        public async Task<ActionResult> Index()
        {
            products = new List<Product>();

            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:63532/api/products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }

            return View(products);
        }

        public async Task<ActionResult> GetById(int productID)
        {
            product = new Product();

            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                using (var response = await httpClient.GetAsync($"http://localhost:63532/api/products/{productID}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }

            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Product product)
        {
            product = new Product();

            using (var httpClient = new HttpClient(_httpClientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"http://localhost:63532/api/products", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }

            return View(product);
        }

    }
}
