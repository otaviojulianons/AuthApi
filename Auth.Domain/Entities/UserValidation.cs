using System;
using FluentValidation;

namespace Auth.Domain.Entities
{
    public class UserValidation : AbstractValidator<UserDomain>
    {
        public UserValidation()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .When( x => !x.Admin);

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(8,8)
                .Matches("^[a-zA-Z0-9]*$");

            RuleFor(x => x.Permissions).NotNull();

            RuleForEach(x => x.Permissions)
                .Matches("^[a-zA-Z0-9]*$")
                    .WithName("Permissions")
                    .WithMessage("Permission format '{PropertyValue}' is invalid.");
        }
    }
}