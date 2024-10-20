using FluentValidation;
using Microsoft.AspNetCore.Rewrite;

namespace InterviewTask.API.Feature.Products.CreateProduct
{
    public class CreateProductValidators : AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidators()
        {
            RuleFor(x => x.Name)
                     .NotEmpty();


            RuleFor(x => x.Description)
                    .NotEmpty();

        }
    }
}
