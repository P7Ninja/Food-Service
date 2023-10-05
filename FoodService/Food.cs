namespace InventoryService
{
    public class Food
    {
        public Food(int id, string name, string description, float price, float discount, string vendor, string category, string subCategory, int macroID)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Discount = discount;
            Vendor = vendor;
            Category = category;
            SubCategory = subCategory;
            MacroID = macroID;
        }
        public Food(int id, string name, float price, float discount)
        {
            Id = id;
            Name = name;
            Price = price;
            Discount = discount;
        }
        public Food() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public string? Vendor { get; set; }
        public string? Category {  get; set; }
        public string? SubCategory {  get; set; }
        public int? MacroID { get; set; }
    }
}