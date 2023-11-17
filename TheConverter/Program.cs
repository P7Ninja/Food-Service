// See https://aka.ms/new-console-template for more information

using FoodService;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using TheConverter;

var fileName = "C://Users//Andre//Downloads//scrape-20231002T071003Z-001//scrape//data//ingredients.json";

var jsonString = File.ReadAllText(fileName);

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var jsonFoods = System.Text.Json.JsonSerializer.Deserialize<List<JsonFood>>(jsonString, options);

var foods = new List<Food>();

foreach (var item in jsonFoods)
{
    var f = new Food
    {
        Name = item.Name,
        Vendor = item.Ref.Name,
        Price = item.UnitPrice,
        PriceKg = item.PriceKg,
        Cal = item.Macro.Calories,
        Fat = item.Macro.Fat,
        Protein = item.Macro.Protein,
        Carbs = item.Macro.Carbohydrates,
        Discount = 0,
        Category = string.Empty
    };
    if (item.Categories.ValueKind is JsonValueKind.String)
    {
        f.Category = item.Categories.ToString();
    }
    else
    {
        var arr = item.Categories.EnumerateArray();
        foreach (var cat in arr)
        {
            f.Category += cat.GetString() + ", ";
        }
        f.Category = f.Category.Trim();
        f.Category = f.Category.Trim(',');
    }
    foods.Add(f);
}


var client = new HttpClient();
string json = JsonConvert.SerializeObject(foods);
var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
var httpResponse = await client.PostAsync("https://localhost:7088/api/foods/addlist", httpContent);

if (httpResponse.IsSuccessStatusCode)
{
    Console.WriteLine("yesyes");
    Console.ReadKey();
}
else
{
    Console.WriteLine(httpResponse.ToString());
}