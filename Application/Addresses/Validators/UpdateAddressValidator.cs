using FluentValidation;
using Ecommerce.Api.Application.Addresses.Dtos;

public class UpdateAddressValidator : AbstractValidator<UpdateAddressRequest>
{
    public UpdateAddressValidator()
    {
        RuleFor(x => x.Label).NotEmpty();
        RuleFor(x => x.DeliveryAddress).NotEmpty();
        RuleFor(x => x.AddressType)
            .Must(type => new[] { "House", "Office", "Apartment", "Other" }.Contains(type))
            .WithMessage("Invalid address type");
    }
}
