namespace InterviewTask.API.Domain.Entities
{
    public sealed class Warehouse : BaseEntity
    {
      public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;


        public ICollection<InventoryTransaction> SourceTransactions { get; set; } = default!;
        public ICollection<InventoryTransaction> DestinationTransactions { get; set; } = default!;

    }
}
