using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingProject.Models;

namespace ShoppingProject.Controllers
{
    public class ProductCategoryController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49301");
            MediaTypeWithQualityHeaderValue contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync("/api/productcategory").Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            List<ProductCategory> data = JsonConvert.DeserializeObject<List<ProductCategory>>(stringData);
            return View(data);
        }
    }
}