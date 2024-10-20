namespace InterviewTask.API.Feature.InventoryTransaction.TransferStock
{
    public record TransferStockRequest
        (

            int SourceWarehouseId,
            int DestinationWarehouseId, 
            int Quantity

        );
}
