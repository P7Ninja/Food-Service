namespace InventoryService
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public int Amount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
