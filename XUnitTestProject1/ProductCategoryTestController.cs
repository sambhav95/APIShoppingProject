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
    public class ProductCategoryTestController
    {
        private ShopDataDbContext context;
        public static DbContextOptions<ShopDataDbContext> dbContextOptions { get; set; }

        public static string connectionString =
           "Data Source=TRD-518; Initial Catalog = ShoppingProjectFinal; Integrated Security = true;";
        static ProductCategoryTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopDataDbContext>()
                    .UseSqlServer(connectionString).Options;
        }
        public ProductCategoryTestController()
        {
            context = new ShopDataDbContext(dbContextOptions);
        }
        [Fact]


        public async void Task_GetProductCategoryBy_Id_Return_OkResult()
        {
            var controller = new ProductCategoryController(context);
            var ProductCategoryId = 1;
            var data = await controller.Get(ProductCategoryId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetProductCategoryBy_Id_Return_NoResult()
        {
            var controller = new ProductCategoryController(context);
            var ProductCategoryId = 6;
            var data = await controller.Get(ProductCategoryId);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_GetProductCategoryById_MatchResult()
        {
            var controller = new ProductCategoryController(context);
            int id = 1;
            var data = await controller.Get(id);
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var prod= okResult.Value.Should().BeAssignableTo<ProductCategory>().Subject;
            Assert.Equal("Summer Collection", prod.CategoryName);
            Assert.Equal("Cotton", prod.CategoryDescription);

        }
        [Fact]
        public async void Task_GetProductCategoryById_BadResult()
        {
            var controller = new ProductCategoryController(context);
            int? id = null;
            var data = await controller.Get(id);
            Assert.IsType<BadRequestResult>(data);

        }
        [Fact]
        public async void Task_Add_AddCategory_Return_OkResult()
        {
            var controller = new ProductCategoryController(context);
            var prod = new ProductCategory()
            {
                CategoryName = "Summer Collection",
                CategoryDescription = "Cotton Material",
               
            };
            var data = await controller.Post(prod);
            Assert.IsType<CreatedAtActionResult>(data);

        }
        //[Fact]
        //public async void Task_Add_Invalid_AddCategory_Return_Ok_BadResult()
        //{
        //    var controller = new ProductCategoryController(context);
        //    var productcategory = new ProductCategory()
        //    {
        //       CategoryName = "springccollection",
        //        CategoryDescription="Floral Collection",
             
        //    };
        //    var data = await controller.Post(productcategory);
        //    Assert.IsType<BadRequestResult>(data);

        //}
        [Fact]
        public async void Task_DeleteCategory_Return_OkResult()
        {
            var controller = new ProductCategoryController(context);
            var id =3;
            var data = await controller.Delete(id);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_DeleteCategory_Return_BAdResult()
        {
            var controller = new ProductCategoryController(context);
            int? id = null;
            var data = await controller.Delete(id);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_DeleteCategory_Return_NotFound()
        {
            var controller = new ProductCategoryController(context);
            int? id = 5;
            var data = await controller.Delete(id);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_Put_CategoryById_OKResult()
        {
            var controller = new ProductCategoryController(context);
            int id =3;
            var productCategory = new ProductCategory()
            {
                ProductCategoryId = 3,
                CategoryName = "Summer Collection",
                CategoryDescription = "Cotton Material"

            };
            var data = await controller.Put(id, productCategory);
            Assert.IsType<NoContentResult>(data);
        }
        [Fact]
        public async void Task_Put_CategoryBYID_Return_BadResult()
        {
            var controller = new ProductCategoryController(context);
            int? id = null;
            var productCategory = new ProductCategory()
            {
                //UserId = null,
                CategoryName = "summmerrrrrr",
                CategoryDescription ="Collections"

            };
            var data = await controller.Put(id, productCategory);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_Return_GetAllProductCategory()
        {
            var controller = new ProductCategoryController(context);
            var data = await controller.Get();
            Assert.IsType<OkObjectResult>(data);
        }
    }
}

