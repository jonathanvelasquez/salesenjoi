using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using salesenjoi.API.Data;
using salesenjoi.API.Entities;

namespace salesenjoi.API.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Categories.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete("id:int")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var afectRow = await _context.Categories.Where(x => x.id == id).ExecuteDeleteAsync();

            if (afectRow == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
