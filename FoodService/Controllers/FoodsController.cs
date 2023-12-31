﻿using FoodService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace FoodService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodsController : ControllerBase
{
    private readonly FoodServiceContext _context;

    public FoodsController(FoodServiceContext context)
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
            .Where(f => f.Name.StartsWith(query))
            .GroupBy(f => f.Name)
            .OrderBy(f => f.Min(n => n.Name.Length))
            .Select(f => f.FirstOrDefault())
            .Take(20)
            .ToListAsync());
    }

    // GET: api/Foods/Discounted
    [HttpGet("Discounted")]
    [OutputCache]
    public async Task<ActionResult<IEnumerable<Food>>> GetDiscountedFoods(string zipcode)
    {
        var foodWasteUrl = $"https://api.sallinggroup.com/v1/food-waste?zip={zipcode}";
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer dc83c623-d474-4c50-bc27-4a7791042539");
        var response = await client.GetAsync(foodWasteUrl);
        var str = await response.Content.ReadAsStringAsync();

        var json = JsonSerializer.Deserialize<List<Root>>(str, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var discounts = new List<DiscountDTO>();
        var i = 0;
        foreach (var item in json)
        {
            foreach (var clearance in item.Clearances)
            {
                try
                {
                    var food = new DiscountDTO()
                    {
                        Id = i,
                        Discount = (float)clearance.Offer.Discount,
                        Price = (float)clearance.Offer.OriginalPrice,
                        Name = clearance.Product.Description,
                        Vendor = item.Store.Name,
                        Category = clearance.Product.Categories.Da,
                    };

                    i++;
                    discounts.Add(food);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        return Ok(discounts);
    }

    // POST: api/Foods/list
    [HttpPost("list")]
    public async Task<ActionResult<IEnumerable<Food>>> GetFoodList(List<int> ids)
    {
        if (_context.Foods == null)
        {
            return NotFound();
        }

        return Ok(await _context.Foods
            .Where(f => ids.Contains(f.Id))
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
        await _context.Foods.AddAsync(food);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFood", new { id = food.Id }, food);
    }

    // Post a list of foods at once
    // POST api/AddList
    [HttpPost("AddList")]
    public async Task<ActionResult<IEnumerable<Food>>> PostFoods(List<Food> foods)
    {
        await _context.Foods.AddRangeAsync(foods);
        await _context.SaveChangesAsync();
        return Ok(foods);
    }

    // DELETE: api/Foods/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFood(int id)
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
    // Delete all the items in foods
    // DELETE: api/foods
    [HttpDelete]
    public async Task<ActionResult> DeleteFoods()
    {
        if (_context.Foods == null)
        {
            return NotFound();
        }

        _context.Foods.RemoveRange(_context.Foods);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FoodExists(int id)
    {
        return (_context.Foods?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
