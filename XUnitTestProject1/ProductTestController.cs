using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Controllers;
using ProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject1
{
  public  class ProductTestController
    {
        private ShopDataDbContext _context;

        public static DbContextOptions<ShopDataDbContext>
            dbContextOptions
        { get; set; }

        public static string connectionString =
  "Data Source=TRD-518; Initial Catalog = ShoppingProjectFinal; Integrated Security = true;";
        static ProductTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopDataDbContext>()
                .UseSqlServer(connectionString).Options;
        }
        public ProductTestController()
        {
            _context = new ShopDataDbContext(dbContextOptions);
        }
        [Fact]
        public async void Task_GetPById_Return_OkResult()
        {
            var controller = new ProductController(_context);
            var PcId = 1;
            var data = await controller.Get(PcId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetpcById_Return_FailResult()
        {
            var controller = new ProductController(_context);
            var PcId = 12;
            var data = await controller.Get(PcId);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_GetProductById_MatchResult()
        {
            var controller = new ProductController(_context);
            int id = 25;
            var data = await controller.Get(id);
            Assert.IsType<OkObjectResult>(data);
            var OkResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var user = OkResult.Value.Should().BeAssignableTo<Product>().Subject;

            Assert.Equal("Saree", user.ProductName);
            Assert.Equal(100, user.ProductQty);
            Assert.Equal(5799, user.ProductPrice);
            Assert.Equal("https://assets.myntassets.com/h_1440,q_100,w_1080/v1/assets/images/2133537/2017/9/20/11505885340667-Ishin-Chanderi-Silk-Cotton-Black-Solid-Party-Wear-Bollywood-New-Collection-With-Golden-Zari-Border-Trendy-Womens-Saree-9201505885340560-1.jpg", user.ProductImage);
            Assert.Equal("for ladies", user.ProductDescription);
            Assert.Equal(2, user.VendorId);
            Assert.Equal(1, user.ProductCategoryId);
            Assert.Equal(1, user.BrandId);

        }

        [Fact]
        public async void Task_getPostById_return_BadRequestResult()
        {
            var controller = new ProductController(_context);
            int? id = null;

            var data = await controller.Get(id);
            Assert.IsType<BadRequestResult>(data);

        }
        [Fact]
        public async void Task_Add_AddPc_Return_OkResult()
        {
            var controller = new ProductController(_context);
            var user = new Product()
            {
                ProductName = "Trial",
                ProductQty = 100,
                ProductPrice = 1900,
                ProductImage = "NULL",
                ProductDescription = "traildone",
                VendorId = 2,
                ProductCategoryId = 3,
                BrandId=1
                
            };
            var data = await controller.Post(user);
            Assert.IsType<CreatedAtActionResult>(data);
        }
        //[Fact]
        //public async void Task_Add_InvalidAddPC_Return_BadResult()
        //{
        //    var controller = new ProductCategoryController(_context);
        //    var user = new ProductCategory()
        //    {
        //        CategoryName = "Sum",
        //        CategoryDescription = "Shirts",

        //    };
        //    var data = await controller.Post(user);
        //    Assert.IsType<BadRequestResult>(data);
        //}
        [Fact]
        public async void Task_DeleteUser_return_OkResult()
        {
            var controller = new ProductController(_context);
            var id = 29;
            var data = await controller.Delete(id);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_Delete_return_NotFoundResult()
        {
            var controller = new ProductController(_context);
            var id = 18;
            var data = await controller.Delete(id);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_DeleteUser_return_Badrequest()
        {
            var controller = new ProductController(_context);
            int? id = null;
            var data = await controller.Delete(id);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_OkResult()
        {
            var controller = new ProductController(_context);
            int id =25;

            var prod = new Product()
            {
                ProductId = 25,
                ProductName = "Saree",
                ProductQty = 100,
                ProductPrice = 5799,
                ProductImage = "https://assets.myntassets.com/h_1440,q_100,w_1080/v1/assets/images/2133537/2017/9/20/11505885340667-Ishin-Chanderi-Silk-Cotton-Black-Solid-Party-Wear-Bollywood-New-Collection-With-Golden-Zari-Border-Trendy-Womens-Saree-9201505885340560-1.jpg",
                ProductDescription = "for ladies",
                VendorId = 2,
                ProductCategoryId = 1,
                BrandId=1
            };
            var data = await controller.Put(id, prod);
            Assert.IsType<NoContentResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_NotFound()
        {
            var controller = new ProductController(_context);
            int? id = 4;

            var prod = new Product()
            {
                ProductId = 25,
                ProductName = "Saree",
                ProductQty = 100,
                ProductPrice = 5799,
                ProductImage = "https://assets.myntassets.com/h_1440,q_100,w_1080/v1/assets/images/2133537/2017/9/20/11505885340667-Ishin-Chanderi-Silk-Cotton-Black-Solid-Party-Wear-Bollywood-New-Collection-With-Golden-Zari-Border-Trendy-Womens-Saree-9201505885340560-1.jpg",
                ProductDescription = "for ladies",
                VendorId = 2,
                ProductCategoryId = 1,
                BrandId=1
            };
            var data = await controller.Put(id, prod);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_BadResult()
        {
            var controller = new ProductController(_context);
            int? id = null;

            var user = new Product()
            {
                ProductId = 4,
                ProductName = "Saree",
                ProductQty = 100,
                ProductPrice = 5799,
                ProductImage = "https://assets.myntassets.com/h_1440,q_100,w_1080/v1/assets/images/2133537/2017/9/20/11505885340667-Ishin-Chanderi-Silk-Cotton-Black-Solid-Party-Wear-Bollywood-New-Collection-With-Golden-Zari-Border-Trendy-Womens-Saree-9201505885340560-1.jpg",
                ProductDescription = "for ladies",
                VendorId = 1,
                ProductCategoryId = 2
            };
            var data = await controller.Put(id, user);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_Return_GetAllProduct()
        {
            var controller = new ProductController(_context);
            var data = await controller.Get();
            Assert.IsType<OkObjectResult>(data);
        }
    }
}


