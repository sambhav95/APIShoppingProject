using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Controllers;
using ProjectAPI.Models;
using System;
using Xunit;

namespace XUnitTestProject1
{
    public class VendorTestController
    {
        private ShopDataDbContext context;
        public static DbContextOptions<ShopDataDbContext> dbContextOptions { get; set; }

        public static string connectionString =
           "Data Source=TRD-518; Initial Catalog =ShoppingProjectFinal; Integrated Security = true;";
        static VendorTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopDataDbContext>()
                    .UseSqlServer(connectionString).Options;
        }
        public VendorTestController()
        {
            context = new ShopDataDbContext(dbContextOptions);
        }
        [Fact]
        

        public async void Task_GetVendorBy_Id_Return_OkResult()
        {
            var controller = new VendorController(context);
            var VendorId = 1;
            var data = await controller.Get(VendorId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetVendorBy_Id_Return_NoResult()
        {
            var controller = new VendorController(context);
            var VendorId = 6;
            var data = await controller.Get(VendorId);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_GetVendorById_MatchResult()
        {
            var controller = new VendorController(context);
            int id = 4;
            var data = await controller.Get(id);
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var vend = okResult.Value.Should().BeAssignableTo<Vendor>().Subject;
            Assert.Equal("Sambhav", vend.VendorName);
            Assert.Equal("sa@gmail.com", vend.EmailId);
            Assert.Equal(9856324155, vend.PhoneNo);
            Assert.Equal("Nice!!", vend.VendorDescription);
          
        }
        [Fact]
        public async void Task_GetVendorById_BadResult()
        {
            var controller = new VendorController(context);
            int? id = null;
            var data = await controller.Get(id);
            Assert.IsType<BadRequestResult>(data);

        }
        [Fact]
        public async void Task_Add_AddVendor_Return_OkResult()
        {
            var controller = new VendorController(context);
            var vend = new Vendor()
            {
                VendorName = "Naman ",
               EmailId = "Naman@gmail.com",
               PhoneNo=9874525867,
               VendorDescription="Good"
              
            };
            var data = await controller.Post(vend);
            Assert.IsType<CreatedAtActionResult>(data);

        }
        [Fact]
        public async void Task_Add_Invalid_AddVendor_Return_Ok_BadResult()
        {
            var controller = new VendorController(context);
            var vend = new Vendor()
            {
                VendorName = "Himanshi Chamoli",
                EmailId = "himi@gmial.com",
                PhoneNo = 9856741585,
                VendorDescription = "Nice!!"
            };
            var data = await controller.Post(vend);
            Assert.IsType<BadRequestResult>(data);

        }
        [Fact]
        public async void Task_Delete_Return_OkResult()
        {
            var controller = new VendorController(context);
            var id = 13;
            var data = await controller.Delete(id);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_DeleteVendor_Return_BAdResult()
        {
            var controller = new VendorController(context);
            int? id = null;
            var data = await controller.Delete(id);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_DeleteVendor_Return_NotFound()
        {
            var controller = new VendorController(context);
            int? id = 25;
            var data = await controller.Delete(id);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_Put_VendorById_OKResult()
        {
            var controller = new VendorController(context);
            int id = 2;
            var vendor = new Vendor()
            {
               VendorId =2,
               VendorName ="Himanshi",
               EmailId = "Himi@gmail.com",
               PhoneNo = 890878767,
               VendorDescription = "Great!"
            };
            var data = await controller.Put(id, vendor);
            Assert.IsType<NoContentResult>(data);
        }
        [Fact]
        public async void Task_Put_VendorBYID_Return_BadResult()
        {
            var controller = new VendorController(context);
            int? id = null;
            var vendor = new Vendor()
            {
             VendorName ="tani",
             EmailId="tani@gmail.com",
             PhoneNo=1236547890,
             VendorDescription="good",
            };
            var data = await controller.Put(id, vendor);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_Return_GetAllVendor()
        {
            var controller = new VendorController(context);
            var data = await controller.Get();
            Assert.IsType<OkObjectResult>(data);
        }
    }
}
