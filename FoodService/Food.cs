namespace FoodService;

public class Food
{
    public Food(int id, string name, float price, float discount, string vendor, string category)
    {
        Id = id;
        Name = name;
        Price = price;
        Discount = discount;
        Vendor = vendor;
        Category = category;
    }

    public Food()
    {
        Id = 0;
        Name = "Food";
        Price = 0;
        Discount = 0;
        Vendor = "Vendor";
        Category = "Category";
        Fat = 0;
        Carbs = 0;
        Protein = 0;
        Cal = 0;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public float PriceKg { get; set; }
    public float Discount { get; set; }
    public string Vendor { get; set; }
    public string Category { get; set; }
    public float Fat { get; set; }
    public float Carbs { get; set; }
    public float Protein { get; set; }
    public float Cal { get; set; }
}