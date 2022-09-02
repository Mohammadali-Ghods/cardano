using ExternalApi.Models;

namespace CardanoCalculationBackend.Models
{
    public class CsvModel
    {
        public string? transaction_uti { get; set; }
        public string? isin { get; set; }
        public decimal notional { get; set; }
        public string? notional_currency { get; set; }
        public string? transaction_type { get; set; }
        public DateTime? transaction_datetime { get; set; }
        public decimal rate { get; set; }
        public string? lei { get; set; }
        public LegalName? legalname { get; set; }
        public string? bic { get; set; }
        public decimal transaction_costs { get; set; }

    }
}
