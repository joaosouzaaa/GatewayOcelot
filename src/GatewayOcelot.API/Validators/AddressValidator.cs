using FluentValidation;
using GatewayOcelot.API.Entities;

namespace GatewayOcelot.API.Validators;

public sealed class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(a => a.ZipCode).Matches(@"^\d{8}$");

        RuleFor(a => a.Number).Length(1, 10);

        When(c => c.Complement is not null, () =>
        {
            RuleFor(a => a.Complement).Length(1, 100);
        });
    }
}
