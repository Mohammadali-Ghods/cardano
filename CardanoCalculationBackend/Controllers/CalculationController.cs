using CardanoCalculationBackend.Models;
using CardanoCalculationBackend.Services;
using ExternalApi.Interface;
using ExternalApi.Models;
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
        public async Task<ContentResult> CaculateAndGetCsv([FromForm] IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                List<CsvModel> listOfData = new List<CsvModel>();
                reader.ReadLine();

                while (reader.Peek() >= 0)
                {
                    CsvModel data = _csvService.ConvertCsvLineIntoObject(reader.ReadLine());
                    listOfData.Add(data);
                }

                var results = await _cardanoApi.GetRecords(listOfData.Select(x => x.lei)
                    .Distinct().ToArray());

                if (results.IsValid().IsValid)
                {
                    for (int i = 0; i <= listOfData.Count - 1; i++)
                    {
                        if (!listOfData[i].IsValid().IsValid)
                            continue;

                        var searchinresult = results.data.
                            Where(x => x.attributes.lei == listOfData[i].lei).FirstOrDefault();

                        if (!searchinresult.IsValid().IsValid)
                        {
                            listOfData[i].Error = searchinresult.Error;
                            continue;
                        }

                        listOfData[i].name = searchinresult.attributes.entity.legalName.name;
                        listOfData[i].language = searchinresult.attributes.entity.legalName.language;
                        listOfData[i].bic = searchinresult.attributes.bic[0];
                        listOfData[i].transaction_costs =
                            _calculatorService.CalculateTransactioCost(listOfData[i].rate, listOfData[i].notional,
                            searchinresult.attributes.entity.legalAddress.country);
                    }

                    string outPutFile =
                               ServiceStack.Text.CsvSerializer.SerializeToCsv<CsvModel>(listOfData);

                    return base.Content(outPutFile, "text/csv");
                }
                else return base.Content("Some thing wrong in your input CSV please check it","text/html");
            }
        }
    }
}
