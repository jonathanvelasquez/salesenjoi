using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using salesenjoi.API.Data;
using salesenjoi.API.Entities;

namespace salesenjoi.API.Controllers
{
    [ApiController]
    [Route("/api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context) 
        { 
            _context = context;
        }

        [HttpGet]   
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.Countries.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Country country)
        {
            _context.Add(country);
            await _context.SaveChangesAsync();
            return Ok(country);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Country country)
        {
            _context.Update(country);
            await _context.SaveChangesAsync();
            return Ok(country);
        }

        [HttpDelete("id:int")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var afectRow = await _context.Countries.Where(x => x.id == id).ExecuteDeleteAsync();

            if (afectRow == 0)
            {
                return NotFound();
            }

            return NoContent(); 
        }

    }
}
