using FluentValidation;

namespace InterviewTask.API.Feature.Products.UpdateProduct
{
    public class UpdateProductValidators : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidators()
        {
            RuleFor(x => x.Name)
                   .NotEmpty();


            RuleFor(x => x.Description)
                    .NotEmpty();

            RuleFor(x => x.Quntatity > 0);
        }
    }
}
