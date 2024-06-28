using FluentValidation;
using GatewayOcelot.Products.API.Entities;

namespace GatewayOcelot.Products.API.Validators;

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
