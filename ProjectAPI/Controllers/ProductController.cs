using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace ProjectAPI.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : Controller
    {
        // ShopDataDbContext context = new ShopDataDbContext();
        private readonly ShopDataDbContext _context;

        public ProductController(ShopDataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Product> prod = await _context.Products.ToListAsync();
            if (prod != null)
            {
                return Ok(prod);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
        }
        //[EnableCors("AllowMyOrigin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody]Product product)
        {

            if (id != product.ProductId)
            {
                return BadRequest();

            }
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();


            return NoContent();
        }
    }
}