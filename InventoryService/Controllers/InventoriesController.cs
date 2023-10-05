﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryService;
using InventoryService.Data;
using System.Reflection.Metadata;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly InventoryServiceContext _context;

        public InventoriesController(InventoryServiceContext context)
        {
            _context = context;
        }

        // GET: api/Inventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventories()
        {
          if (_context.Inventories == null)
          {
              return NotFound();
          }
            return await _context.Inventories.Include(i => i.Items).ToListAsync();
        }

        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
          if (_context.Inventories == null)
          {
              return NotFound();
          }
            var inventory = await _context.Inventories.Include(i => i.Items).Where(i => i.Id == id).FirstOrDefaultAsync();

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        // PUT: api/Inventories/5
        // Can only update already existing inventoryItems. Does not delete or add new!!
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            _context.Update(inventory);
            _context.UpdateRange(inventory.Items);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // POST: api/Inventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<IActionResult> AddToInventory(int id, InventoryItem item)
        {
            var entity = await _context.Inventories.FindAsync(id);
            
            if (entity == null)
            {
                return NotFound();
            }

            entity.Items.Add(item);

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // POST: api/Inventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
          if (_context.Inventories == null)
          {
              return Problem("Entity set 'InventoryServiceContext.Inventories'  is null.");
          }
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventory", new { id = inventory.Id }, inventory);
        }

        // DELETE: api/Inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            if (_context.Inventories == null)
            {
                return NotFound();
            }
            var inventory = await _context.Inventories.Include(i => i.Items).FirstAsync(i => i.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete: api/Inventories/5/5
        [HttpDelete("{inventoryId}/{itemId}")]
        public async Task<IActionResult> DeleteItemFromInventory(int inventoryId, int itemId)
        {
            var entry = await _context.InventoryItems.FindAsync(itemId);

            if (entry == null)
            {
                return NotFound();
            }

            _context.Remove(entry);

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(inventoryId))
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

        private bool InventoryExists(int id)
        {
            return (_context.Inventories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
