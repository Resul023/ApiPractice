using FluentValidation;

namespace StoreApi.Apps.AdminApp.DTOs.CategoryDtos
{
    public class CategoryPostDto
    {
        public string Name { get; set; }
    }
    public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(20).WithMessage("Name lenght can not be greater than 20")
                .NotNull().WithMessage("Name is required");
        }
    }
}
