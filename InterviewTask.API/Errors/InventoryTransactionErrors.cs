using InterviewTask.API.Abstraction;

namespace InterviewTask.API.Errors
{
    public static class InventoryTransactionErrors
    {
        public static readonly Error InventoryTransactionNotFound
               = new("InventoryTransaction.NotFound", "InventoryTransaction not found");

        

    }
}
