using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryService;

namespace InventoryService.Data
{
    public class InventoryServiceContext : DbContext
    {
        public InventoryServiceContext (DbContextOptions<InventoryServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
    }
}