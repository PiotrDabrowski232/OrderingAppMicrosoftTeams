using FluentValidation;
using OrderingApp.Data.Models.Enum;
using OrderingApp.Restaurant.Logic.DTO;

namespace OrderingApp.Restaurant.Logic.Validation
{
    public class NewDishDtoValidation : AbstractValidator<NewDishDto>
    {
        public NewDishDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .Length(2, 40)
                .WithMessage("The name should contain from 2 to 40 characters");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .Length(30, 300)
                .WithMessage("The name should contain from 30 to 300 characters");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price is required")
                .GreaterThan(0)
                .WithMessage("Price must be grater than 0");

            RuleFor(x => x.Calories)
                .NotEmpty()
                .WithMessage("Price is required")
                .GreaterThan(0)
                .WithMessage("Price must be grater than 0");
        }
    }
}
