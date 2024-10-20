using FluentValidation;
using OrderingApp.Data.Models.Enum;
using OrderingApp.Restaurant.Logic.DTO;

namespace OrderingApp.Restaurant.Logic.Validation
{
    public class RestaurantDtoValidation : AbstractValidator<RestaurantDto>
    {
        public RestaurantDtoValidation() {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name Is Required.")
                .Length(2, 100)
                .WithMessage("The name should contain from 2 to 100 characters");

            RuleFor(x => x.RestaurantType)
                .NotEmpty()
                .WithMessage("type is required.")
                .IsEnumName(typeof(RestauranType))
                .WithMessage("invalid type.");
        }
    }
}
