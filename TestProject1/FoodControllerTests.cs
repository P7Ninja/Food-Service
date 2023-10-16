using FoodService;
using FoodService.Controllers;
using FoodService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace TestProject;

public class FoodControllerTests
{
    private FoodsController controller;
    private Food food;

    [SetUp]
    public void Setup()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var optionsBuilder = new DbContextOptionsBuilder<FoodServiceContext>().UseSqlite(connection);
        var context = new FoodServiceContext(optionsBuilder.Options);
        context.Database.EnsureCreated();
        controller = new FoodsController(context);
        food = new Food();
    }

    [Test]
    public async Task Post()
    {
        var response = await controller.PostFood(food);

        Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
    }

    [Test]
    public async Task PostNegativePrice()
    {
        food.Price = -5;
        var response = await controller.PostFood(food);

        Assert.IsInstanceOf<BadRequestObjectResult>(response.Result);
    }

    [Test]
    public async Task Get()
    {
        await controller.PostFood(food);
        var response = await controller.GetFood(food.Id);

        Assert.IsInstanceOf<OkObjectResult>(response.Result);
    }

    [Test]
    public async Task Delete()
    {
        await controller.PostFood(food);

        var deleteResponse = await controller.DeleteFood(food.Id);
        Assert.IsInstanceOf<NoContentResult>(deleteResponse);

        var getResponse = await controller.GetFood(food.Id);
        Assert.IsInstanceOf<NotFoundResult>(getResponse.Result);

    }

    [Test]
    public async Task GetWithQuerySuccess()
    {
        Food food1 = new Food();
        food1.Name = "Tomat";
        Food food2 = new Food();
        food2.Name = "Æble";
        await controller.PostFood(food);
        await controller.PostFood(food);
        var response = await controller.GetFoods("Æbl");

        Assert.IsInstanceOf<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;

        Assert.That((result.Value as IEnumerable<Food>).Count() == 1);
    }

    [Test]
    public async Task GetWithQueryNoMatch()
    {
        Food food1 = new Food();
        food1.Name = "Tomat";
        Food food2 = new Food();
        food2.Name = "Æble";
        await controller.PostFood(food1);
        await controller.PostFood(food2);
        var response = await controller.GetFoods("This should not return anything");

        Assert.IsInstanceOf<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;

        Assert.That((result.Value as IEnumerable<Food>).Count() == 0);
    }
}