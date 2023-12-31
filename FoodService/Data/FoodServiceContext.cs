﻿using Microsoft.EntityFrameworkCore;

namespace FoodService.Data;

public class FoodServiceContext : DbContext
{
    public FoodServiceContext(DbContextOptions<FoodServiceContext> options)
        : base(options)
    {
    }

    public DbSet<Food> Foods { get; set; }
}