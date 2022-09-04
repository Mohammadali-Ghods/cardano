using CardanoCalculationBackend.Models;
using System.Globalization;

namespace CardanoCalculationBackend.Services
{
    public class CsvService
    {
        public CsvModel ConvertCsvLineIntoObject(string input)
        {
            string[] values = input.Split(',');
            CsvModel model = new CsvModel();
            model.transaction_uti = values[0];
            model.isin = values[1];
            model.notional = decimal.Parse(values[2], NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
            model.notional_currency = values[3];
            model.transaction_type = values[4];
            model.transaction_datetime = Convert.ToDateTime(values[5]);
            model.rate = decimal.Parse(values[6]);
            model.lei = values[7];
            return model;
        }
    }
}
