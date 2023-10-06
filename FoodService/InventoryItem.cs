namespace InventoryService
{
    public class InventoryItem
    {
        public InventoryItem(int id, int foodId, int amount, DateTime expirationDate)
        {
            Id = id;
            FoodId = foodId;
            Amount = amount;
            ExpirationDate = expirationDate;
        }

        public int Id { get; set; }
        public int FoodId { get; set; }
        public Food? Food { get; set; }
        public int Amount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
