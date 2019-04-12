using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace ProjectAPI.Controllers
{  [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        //ShopDataDbContext context = new ShopDataDbContext();

        private readonly ShopDataDbContext _context;

        public VendorController(ShopDataDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Vendor>vend = await _context.Vendors.ToListAsync();
            if(vend!=null)
            {
                return Ok(vend);
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
            var Vendor = await _context.Vendors.FindAsync(id);
            if (Vendor == null)
            {
                return NotFound();
            }

            return Ok(Vendor);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    _context.Vendors.Add(vendor);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("Get", new { id = vendor.VendorId }, vendor);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
               
            }
            
        }
        [EnableCors("AllowMyOrigin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var vendor= await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return Ok(vendor);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody]Vendor vendor)
        {

            if (id != vendor.VendorId)
            {
                return BadRequest();

            }
            _context.Entry(vendor).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}