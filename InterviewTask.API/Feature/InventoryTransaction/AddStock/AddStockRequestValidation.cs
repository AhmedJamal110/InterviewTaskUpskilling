using FluentValidation;

namespace InterviewTask.API.Feature.InventoryTransaction.AddStock
{
    public class AddStockRequestValidation : AbstractValidator<AddStockRequest>
    {
        public AddStockRequestValidation()
        {
            RuleFor(x => x.Quntity > 0);          
        }
    }
}
