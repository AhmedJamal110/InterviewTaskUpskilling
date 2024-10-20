using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using InterviewTask.API.Domain.Enums;

namespace InterviewTask.API.Domain.Entities
{
    public sealed class InventoryTransaction : BaseEntity
    {
        public TransactionType Type { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;


        public int SourceWarehouseId { get; set; }
        public Warehouse SourceWarehouse { get; set; } = default!;

        public int DestinationWarehouseId { get; set; }
        public Warehouse DestinationWarehouse { get; set; } = default!;

    }
}
