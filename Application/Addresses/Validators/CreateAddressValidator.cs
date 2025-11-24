using FluentValidation;

public class CreateAddressValidator : AbstractValidator<CreateAddressRequest>
{
    public CreateAddressValidator()
    {
        RuleFor(x => x.Label).NotEmpty();
        RuleFor(x => x.DeliveryAddress).NotEmpty();
        RuleFor(x => x.AddressType)
            .Must(x => new[] { "House", "Office", "Apartment", "Other" }.Contains(x));
    }
}
