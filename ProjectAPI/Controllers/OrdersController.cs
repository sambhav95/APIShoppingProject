using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ShopDataDbContext _context;

        public OrdersController(ShopDataDbContext context)
        {
            _context = context;
        }
        //ShopDataDbContext _context = new ShopDataDbContext();

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult>GetOrders()
        {
            List<Order> order = await _context.Orders.ToListAsync();
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int? id, [FromBody] Order order)
        {
            if (id == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
        }
        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        private bool OrderExists(int? id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}