// See https://aka.ms/new-console-template for more information

using FoodService;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using TheConverter;

var fileName = "C://Users//Andre//Downloads//scrape-20231002T071003Z-001//scrape//data//fotex//ingredients.json";

var jsonString = File.ReadAllText(fileName);

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var jsonFoods = System.Text.Json.JsonSerializer.Deserialize<List<JsonFoodFøtex>>(jsonString, options);

var foods = new List<Food>();

foreach (var item in jsonFoods)
{
    var f = new Food();
    f.Name = item.Name;
    f.Vendor = item.Ref.Name;
    f.Price = item.UnitPrice;
    f.PriceKg = item.PriceKg;
    f.Cal = item.Macro.Calories;
    f.Fat = item.Macro.Fat;
    f.Protein = item.Macro.Protein;
    f.Carbs = item.Macro.Carbohydrates;
    f.Discount = 0;
    f.Category = "";
    foreach (var cat in item.Categories) {
        f.Category += " " + cat;
    }
    foods.Add(f);
}

var client = new HttpClient();
string json = JsonConvert.SerializeObject(foods);
var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
var httpResponse = await client.PostAsync("https://localhost:7088/api/foods/n", httpContent);

if (httpResponse.IsSuccessStatusCode)
{
    Console.WriteLine("yesyes");
    Console.ReadKey();
}