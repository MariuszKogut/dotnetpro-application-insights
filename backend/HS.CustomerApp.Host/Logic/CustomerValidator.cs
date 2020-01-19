using FluentValidation;
using HS.CustomerApp.Host.Models;

namespace HS.CustomerApp.Host.Logic
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotEmpty().Length(3, 255);
            RuleFor(x => x.Location).NotEmpty().Length(3, 100);
        }
    }
}