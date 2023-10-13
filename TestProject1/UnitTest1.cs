using FoodService;
using FoodService.Controllers;
using FoodService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestProject1;

public class Tests
{
    private FoodsController _foodsController;
    //[SetUp]
    //public void Setup()
    //{
    //    var optionsBuilder = new DbContextOptionsBuilder<InventoryServiceContext>().UseInMemoryDatabase("test");
    //    var context = new InventoryServiceContext(optionsBuilder.Options);
    //    _foodsController = new FoodsController(context);
    //}

    [Test]
    public async Task Post()
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventoryServiceContext>().UseInMemoryDatabase("test");
        var context = new InventoryServiceContext(optionsBuilder.Options);
        _foodsController = new FoodsController(context);
        var result = await _foodsController.PostFood(new Food(id: 0, name: "idk", price: 5.5f, discount: 0));
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
    }

    [Test]
    public async Task PostNegativePrice()
    {
        var optionsBuilder1 = new DbContextOptionsBuilder<InventoryServiceContext>().UseInMemoryDatabase("test1");
        var context1 = new InventoryServiceContext(optionsBuilder1.Options);
        _foodsController = new FoodsController(context1);
        var result = await _foodsController.PostFood(new Food(id: 0, name: "idk", price: -5.5f, discount: 0));

        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }

    [Test]
    public async Task Get()
    {
        var optionsBuilder2 = new DbContextOptionsBuilder<InventoryServiceContext>().UseInMemoryDatabase("test2");
        var context2 = new InventoryServiceContext(optionsBuilder2.Options);
        _foodsController = new FoodsController(context2);
        await _foodsController.PostFood(new Food(id: 0, name: "idk", price: 5.5f, discount: 0));
        var result = await _foodsController.GetFood(1);

        Assert.IsInstanceOf<OkObjectResult>(result.Result);
    }

    [Test]
    public async Task Delete()
    {
        var optionsBuilder3 = new DbContextOptionsBuilder<InventoryServiceContext>().UseInMemoryDatabase("test3");
        var context3 = new InventoryServiceContext(optionsBuilder3.Options);
        _foodsController = new FoodsController(context3);
        await _foodsController.PostFood(new Food(id: 0, name: "idk", price: 5.5f, discount: 0));
        var result = await _foodsController.DeleteFood(1);

        Assert.IsInstanceOf<NoContentResult>(result);

        var result2 = await _foodsController.GetFood(1);
        Assert.IsInstanceOf<NotFoundResult>(result2.Result);

    }
}