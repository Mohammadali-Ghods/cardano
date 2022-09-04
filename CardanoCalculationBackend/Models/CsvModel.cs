using CardanoCalculationBackend.Validations;
using ExternalApi.Models;
using FluentValidation.Results;

namespace CardanoCalculationBackend.Models
{
    public class CsvModel
    {
        public string? transaction_uti { get; set; }
        public string? isin { get; set; }
        public decimal notional { get; set; }
        public string? notional_currency { get; set; }
        public string? transaction_type { get; set; }
        public DateTime transaction_datetime { get; set; }
        public decimal rate { get; set; }
        public string? lei { get; set; }
        public string? name { get; set; }
        public string? language { get; set; }
        public string? bic { get; set; }
        public decimal transaction_costs { get; set; }
        public string? Error { get; set; }

        public ValidationResult IsValid()
        {
            var validationResult = new CsvModelValidation().Validate(this);
            if (!validationResult.IsValid)
                Error = string.Join
                               ("\n", validationResult.Errors.Select(x => x.ErrorMessage).ToArray());
            return new CsvModelValidation().Validate(this);
        }
    }
}
