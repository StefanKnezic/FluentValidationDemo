using FluentValidation;
using FluentValidationDemo.Models;

namespace FluentValidationDemo.Validator
{
    public class PersonValidator : AbstractValidator<PersonModel>
    {
        public PersonValidator()
        {
            RuleFor(p => p.FirstName )
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("You need to type your {PropertyName}!")
                .Length(2, 50).WithMessage("invalid ({TotalLength}) {PropertyName} length!")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");

            RuleFor(x => x.DateBirth).Must(BeAValidAge).WithMessage("Invalid {PropertyName}");

            RuleFor(p => p.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("You need to type your {PropertyName}!")
                .Length(2, 50).WithMessage("invalid ({TotalLength}) {PropertyName} length!")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");

            RuleFor(p => p.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("You need to type your {PropertyName}!")
                .EmailAddress().WithMessage("invalid {PropertyName}!");

            RuleFor(x => x.AccountBalance)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("You need to type your {PropertyName}!")
                .Custom((x, context) =>
                {
                    if ((!(int.TryParse(x, out int value)) || value < 0))
                    {
                        context.AddFailure($"{x} is not a valid number or less than 0");
                    }
                });

        }

        protected bool BeAValidName(string Name)
        {
            Name = Name.Replace(" ","");
            Name = Name.Replace("-", "");

            return Name.All(Char.IsLetter);
        }


        protected bool BeAValidAge(DateTime time)
        {
            int currentYear = DateTime.Now.Year;
            int dbYear = time.Year;

            if( dbYear <= currentYear && dbYear > (currentYear - 120) )
            {
                return true;
            }

            return false;
        }
    }
}
