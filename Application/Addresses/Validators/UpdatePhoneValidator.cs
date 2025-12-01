using Ecommerce.Api.Application.Users.Dtos;
using FluentValidation;

namespace Ecommerce.Api.Application.Addresses.Validators;

public class UpdatePhoneValidator : AbstractValidator<UpdatePhoneRequest>
{
    public UpdatePhoneValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Número de telefone é obrigatório")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Número de telefone inválido");
    }
}