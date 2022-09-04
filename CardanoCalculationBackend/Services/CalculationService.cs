namespace CardanoCalculationBackend.Services
{
    public class CalculationService
    {
        public decimal CalculateTransactioCost(decimal notional, decimal rate, string country)
        {
            if (country == "GB")
                return notional * rate;

            if (country == "NL")
                return Math.Abs(notional * (1 / rate) - notional);

            return 0;
        }
    }
}
