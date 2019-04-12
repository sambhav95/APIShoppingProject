using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EcommerceProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcommerceProject.Controllers
{
    public class ProductController : Controller
    {
      
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49301");
            MediaTypeWithQualityHeaderValue contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync("/api/Product").Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            List<Product> data = JsonConvert.DeserializeObject<List<Product>>(stringData);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49301");
            string stringData = JsonConvert.SerializeObject(product);
            var contentData = new StringContent
                (stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/product", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49301");

            HttpResponseMessage response = client.GetAsync("/api/product/" + id).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            Product product = JsonConvert.DeserializeObject<Product>(stringData);
            return View(product);

        }
    }
}