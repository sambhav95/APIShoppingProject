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
    public class OrderTestController
    {
        private ShopDataDbContext _context;

        public static DbContextOptions<ShopDataDbContext>
            dbContextOptions
        { get; set; }

        public static string connectionString =
  "Data Source=TRD-518; Initial Catalog = ShoppingProjectFinal; Integrated Security = true;";
        static OrderTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopDataDbContext>()
                .UseSqlServer(connectionString).Options;
        }
        public OrderTestController()
        {
            _context = new ShopDataDbContext(dbContextOptions);
        }
        [Fact]
        public async void Task_GetOrderById_Return_OkResult()
        {
            var controller = new OrdersController(_context);
            var OrderId = 1;
            var data = await controller.GetOrder(OrderId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetOrderById_Return_FailResult()
        {
            var controller = new OrdersController(_context);
            var PcId = 12;
            var data = await controller.GetOrder(PcId);
            Assert.IsType<NotFoundResult>(data);
        }
        //[Fact]
        //public async void Task_GetUserById_MatchResult()
        //{
        //    var controller = new OrdersController(_context);
        //    int id = 2;
        //    var data = await controller.GetOrder(id);
        //    Assert.IsType<OkObjectResult>(data);
        //    var OkResult = data.Should().BeOfType<OkObjectResult>().Subject;
        //    var user = OkResult.Value.Should().BeAssignableTo<Product>().Subject;

        //    Assert.Equal("shirt", user.ProductName);
        //    Assert.Equal(700, user.ProductQty);
        //    Assert.Equal(600, user.ProductPrice);
        //    Assert.Equal("https://assets.myntassets.com/h_1440,q_100,w_1080/v1/assets/images/1758578/2017/5/12/11494569522819-WROGN-Men-Shirts-8521494569522516-1.jpg", user.ProductImage);
        //    Assert.Equal("Cotton ", user.ProductDescription);
        //    Assert.Equal(2, user.VendorId);
        //    Assert.Equal(2, user.ProductCategoryId);

        //}

        [Fact]
        public async void Task_getPostById_return_BadRequestResult()
        {
            var controller = new OrdersController(_context);
            int? id = null;

            var data = await controller.GetOrder(id);
            Assert.IsType<BadRequestResult>(data);

        }
        //[Fact]
        //public async void Task_Add_AddPc_Return_OkResult()
        //{
        //    var controller = new OrdersController(_context);
        //    var user = new Product()
        //    {
        //        ProductName = "Trial",
        //        ProductQty = 100,
        //        ProductPrice = 1900,
        //        ProductImage = "NULL",
        //        ProductDescription = "traildone",
        //        VendorId = 1,
        //        ProductCategoryId = 2
        //    };
        //    var data = await controller.PostOrder(user);
        //    Assert.IsType<CreatedAtActionResult>(data);
        //}
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
            var controller = new OrdersController(_context);
            var id = 1;
            var data = await controller.DeleteOrder(id);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_Delete_return_NotFoundResult()
        {
            var controller = new OrdersController(_context);
            var id = 18;
            var data = await controller.DeleteOrder(id);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_DeleteUser_return_Badrequest()
        {
            var controller = new OrdersController(_context);
            int? id = null;
            var data = await controller.DeleteOrder(id);
            Assert.IsType<BadRequestResult>(data);
        }
        //[Fact]
        //public async void Task_PutUserId_OkResult()
        //{
        //    var controller = new OrdersController(_context);
        //    int id = 4;

        //    var prod = new Product()
        //    {
        //        ProductId = 4,
        //        ProductName = "Saree",
        //        ProductQty = 100,
        //        ProductPrice = 5799,
        //        ProductImage = "https://assets.myntassets.com/h_1440,q_100,w_1080/v1/assets/images/2133537/2017/9/20/11505885340667-Ishin-Chanderi-Silk-Cotton-Black-Solid-Party-Wear-Bollywood-New-Collection-With-Golden-Zari-Border-Trendy-Womens-Saree-9201505885340560-1.jpg",
        //        ProductDescription = "for ladies",
        //        VendorId = 1,
        //        ProductCategoryId = 2
        //    };
        //    var data = await controller.PutOrder(id, prod);
        //    Assert.IsType<NoContentResult>(data);
        //}
        //[Fact]
        //public async void Task_PutUserId_NotFound()
        //{
        //    var controller = new OrdersController(_context);
        //    int? id = 5;

        //    var prod = new Product()
        //    {
        //        ProductId = 4,
        //        ProductName = "Saree",
        //        ProductQty = 100,
        //        ProductPrice = 5799,
        //        ProductImage = "https://assets.myntassets.com/h_1440,q_100,w_1080/v1/assets/images/2133537/2017/9/20/11505885340667-Ishin-Chanderi-Silk-Cotton-Black-Solid-Party-Wear-Bollywood-New-Collection-With-Golden-Zari-Border-Trendy-Womens-Saree-9201505885340560-1.jpg",
        //        ProductDescription = "for ladies",
        //        VendorId = 1,
        //        ProductCategoryId = 2
        //    };
        //    var data = await controller.PutOrder(id, prod);
        //    Assert.IsType<NotFoundResult>(data);
        //}
        //[Fact]
        //public async void Task_PutUserId_BadResult()
        //{
        //    var controller = new OrdersController(_context);
        //    int? id = null;

        //    var user = new Product()
        //    {
        //        ProductId = 4,
        //        ProductName = "Saree",
        //        ProductQty = 100,
        //        ProductPrice = 5799,
        //        ProductImage = "https://assets.myntassets.com/h_1440,q_100,w_1080/v1/assets/images/2133537/2017/9/20/11505885340667-Ishin-Chanderi-Silk-Cotton-Black-Solid-Party-Wear-Bollywood-New-Collection-With-Golden-Zari-Border-Trendy-Womens-Saree-9201505885340560-1.jpg",
        //        ProductDescription = "for ladies",
        //        VendorId = 1,
        //        ProductCategoryId = 2
        //    };
        //    var data = await controller.PutOrder(id, user);
        //    Assert.IsType<BadRequestResult>(data);
        //}
        [Fact]
        public async void Task_Return_GetAllProduct()
        {
            var controller = new OrdersController(_context);
            var data = await controller.GetOrders();
            Assert.IsType<OkObjectResult>(data);
        }
    }
}

