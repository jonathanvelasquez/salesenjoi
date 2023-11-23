using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using salesenjoi.API.Data;
using salesenjoi.API.Entities;

namespace salesenjoi.API.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Products.Include(c => c.Category).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("id:int")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var afectRow = await _context.Products.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (afectRow == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
