using CardanoCalculationBackend.Models;
using CardanoCalculationBackend.Services;
using ExternalApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CardanoCalculationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private CsvService _csvService;
        private ICardanoAPI _cardanoApi;
        private CalculationService _calculatorService;
        public CalculationController(CsvService csvService,
            ICardanoAPI cardanoApi,
            CalculationService calculationService)
        {
            _csvService = csvService;
            _cardanoApi = cardanoApi;
            _calculatorService = calculationService;
        }

        [HttpPost]
        public async Task<ContentResult> CalculateCsv([FromForm] IFormFile file)
        {
            List<CsvModel> listOfData = new List<CsvModel>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                reader.ReadLine();

                while (reader.Peek() >= 0)
                {
                    CsvModel data = _csvService.FromCardanoCsv(reader.ReadLine());
                    var result = await _cardanoApi.GetRecord(data.lei);
                    data.legalname = result.data[0].attributes.entity.legalName;
                    data.bic = result.data[0].attributes.bic[0];
                    data.transaction_costs = 
                        _calculatorService.CalculateTransactioCost(data.rate, data.notional,
                        result.data[0].attributes.entity.legalAddress.country);
                    listOfData.Add(data);
                }
            }

            string outPutFile = 
                ServiceStack.Text.CsvSerializer.SerializeToCsv<CsvModel>(listOfData);

            return base.Content(outPutFile, "text/csv");
        }
    }
}
