using FluentValidation;

namespace StoreApi.Apps.AdminApp.DTOs.ProductDtos
{
    public class ProductPostDto
    {
        public string Name  { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }

    }
    public class ProductPostDtoValidator:AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can not be null")
                .MaximumLength(20).WithMessage("Name must be less than 20");

            RuleFor(x => x.SalePrice)
                .GreaterThanOrEqualTo(0).WithMessage("Sale price must be greater than 0")
                .NotNull().WithMessage("Sale price is required");

            RuleFor(x => x.CostPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Cost price must be greater than 0")
                .NotNull().WithMessage("Cost price is required");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.CostPrice > x.SalePrice)
                    context.AddFailure("SalePrice","SalePrice must be greater than CostPrice");
            });
        }
    }
}
