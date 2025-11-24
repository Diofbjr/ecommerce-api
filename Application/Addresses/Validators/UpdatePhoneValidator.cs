using Ecommerce.Api.Application.Users.Dtos;
using FluentValidation;

public class UpdatePhoneValidator : AbstractValidator<UpdatePhoneRequest>
{
    public UpdatePhoneValidator()
    {
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.CountryCode).NotEmpty();
    }
}
