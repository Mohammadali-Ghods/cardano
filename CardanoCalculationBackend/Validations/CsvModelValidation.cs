using CardanoCalculationBackend.Models;
using FluentValidation;

namespace CardanoCalculationBackend.Validations
{
    public class CsvModelValidation: AbstractValidator<CsvModel>
    {
        public CsvModelValidation() 
        {
            RuleFor(x => x.lei).NotNull().NotEmpty()
                .WithMessage("The lei column is empty or null you should fill it")
                .Length(20)
                .WithMessage("The length of lei column is invalid please check the import file");

            RuleFor(x => x.notional).NotNull().NotEmpty()
                .WithMessage("The notional column used for calculate Transaction Cost" +
                " and null and empty value" +
                "for this column is not possible");

            RuleFor(x => x.rate).NotNull().NotEmpty()
                .WithMessage("The rate column used for calculate Transaction " +
                "Cost and null and empty value" +
                "for this column is not possible").GreaterThan(0)
                .LessThan(1).WithMessage("Rate should be number between 0 and 1");

        }
    }
}
