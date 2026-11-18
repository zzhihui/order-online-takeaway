using FluentValidation;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Service.Validations
{
    public interface IWsOptionItemValidator
    {

    }

    public enum WsOptionItemValidatorRuleSet
    {
        Existing
    }


    public class WsOptionItemValidator : AbstractValidator<WsOptionItem>, IWsOptionItemValidator
    {
        public WsOptionItemValidator()
        {
            RuleFor(p => p.ItemName).NotNull().WithMessage("Name Cannot be null");
            RuleFor(p => p.ItemName.Length).LessThanOrEqualTo(30).WithMessage("Name too long").When(r => !string.IsNullOrEmpty(r.ItemName));
            RuleFor(p => p.TranslateName).NotNull().WithMessage("TranslateName Cannot be null");
            RuleFor(p => p.TranslateName.Length).LessThanOrEqualTo(30).WithMessage("TranslateName too long").When(r => !string.IsNullOrEmpty(r.TranslateName));
            RuleFor(p => p.GroupId).GreaterThanOrEqualTo(1).WithMessage("Group Id invalid");
            RuleFor(p => p.GroupId).LessThanOrEqualTo(4).WithMessage("Group Id invalid");

        }
    }
}