namespace InterviewTask.API.Domain.Entities
{
    public sealed class Product : BaseEntity 
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quntatity { get; set; }
        public decimal Price { get; set; }
        public int LowStockThreshold { get; set; }

        
        public ICollection<InventoryTransaction> Transactions { get; set; } = [];
    }
}
