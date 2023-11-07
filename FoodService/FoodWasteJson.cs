namespace FoodService
{
    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
        public object Extra { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
    }

    public class Categories
    {
        public string Da { get; set; }
        public string En { get; set; }
    }

    public class Clearance
    {
        public Offer Offer { get; set; }
        public Product Product { get; set; }
    }

    public class Hour
    {
        public string Date { get; set; }
        public string Type { get; set; }
        public string Open { get; set; }
        public string Close { get; set; }
        public bool Closed { get; set; }
        public List<float> CustomerFlow { get; set; }
    }

    public class Offer
    {
        public string Currency { get; set; }
        public float Discount { get; set; }
        public string Ean { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime LastUpdate { get; set; }
        public float NewPrice { get; set; }
        public float OriginalPrice { get; set; }
        public float PercentDiscount { get; set; }
        public DateTime StartTime { get; set; }
        public float Stock { get; set; }
        public string StockUnit { get; set; }
    }

    public class Product
    {
        public Categories Categories { get; set; }
        public string Description { get; set; }
        public string Ean { get; set; }
        public string Image { get; set; }
    }

    public class Root
    {
        public List<Clearance> Clearances { get; set; }
        public Store Store { get; set; }
    }

    public class Store
    {
        public Address Address { get; set; }
        public string Brand { get; set; }
        public List<float> Coordinates { get; set; }
        public List<Hour> Hours { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
    }
}
