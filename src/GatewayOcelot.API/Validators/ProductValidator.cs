﻿using FluentValidation;
using GatewayOcelot.API.Entities;

namespace GatewayOcelot.API.Validators;

public sealed class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name).Length(1, 150);

        RuleFor(p => p.Description).Length(10, 2000);

        RuleFor(p => p.Price).GreaterThan(0);

        RuleFor(p => p.Address).SetValidator(new AddressValidator());
    }
}
