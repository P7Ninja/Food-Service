using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodService.Data;

namespace FoodService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodsController : ControllerBase
{
    private readonly InventoryServiceContext _context;

    public FoodsController(InventoryServiceContext context)
    {
        _context = context;
    }

    // GET: api/Foods
    // GET: api/Foods?query=x
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Food>>> GetFoods(string? query = null)
    {
        if (_context.Foods == null)
        {
            return NotFound();
        }
        if (query == null)
        {
            return await _context.Foods.ToListAsync();
        }

        return Ok(await _context.Foods
            .Where(f =>f.Name.ToUpper().Contains(query.ToUpper()))
            .Take(20)
            .ToListAsync());
    }

    // GET: api/Foods/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Food>> GetFood(int id)
    {
        if (_context.Foods == null)
        {
            return NotFound();
        }
        var food = await _context.Foods.FindAsync(id);

        if (food == null)
        {
            return NotFound();
        }

        return Ok(food);
    }

    // PUT: api/Foods/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFood(int id, Food food)
    {
        if (id != food.Id)
        {
            return BadRequest();
        }

        _context.Entry(food).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FoodExists(id))
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

    // POST: api/Foods
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Food>> PostFood(Food food)
    {
        if (food.Price < 0)
        {
            return BadRequest("Price cannot be negative.");
        }
        if (_context.Foods == null)
        {
            return Problem("Entity set 'InventoryServiceContext.Foods'  is null.");
        }
        _context.Foods.Add(food);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFood", new { id = food.Id }, food);
    }

    // DELETE: api/Foods/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFood(int id)
    {
        if (_context.Foods == null)
        {
            return NotFound();
        }
        var food = await _context.Foods.FindAsync(id);
        if (food == null)
        {
            return NotFound();
        }

        _context.Foods.Remove(food);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FoodExists(int id)
    {
        return (_context.Foods?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
