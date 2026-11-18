
using Core.Common.Resources;
using FluentValidation;
using Takeaway.Admin.Wpf.ViewModels;

namespace Takeaway.Admin.Wpf.Validation
{
    public class OptionExtraViewModelValidator : AbstractValidator<OptionExtraViewModel>
    {
        public OptionExtraViewModelValidator()
        {
            RuleForEach(subItems => subItems.SubItemSelections).Must((p, subItems) => p.SelectedItem == null).WithMessage(StringResource.OptionExtra_Error);
        }
    }
}