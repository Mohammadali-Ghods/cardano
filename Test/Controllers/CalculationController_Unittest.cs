using CardanoCalculationBackend.Controllers;
using CardanoCalculationBackend.Models;
using CardanoCalculationBackend.Services;
using ExternalApi.Interface;
using ExternalApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;

namespace Test.Controllers
{
    
    public class CalculationController_Unittest
    {
        [Fact]
        public async Task PostCalculation_WithCurrectData() 
        {
            Fixture AutoData = new Fixture();

            var csvService = new CsvService();
            var calculationService = new CalculationService();

            var cardanoApi = new Mock<ICardanoAPI>();
            cardanoApi.Setup(p => p.GetRecords(new string[1] { "XKZZ2JZF41MRHTR1V493" })).ReturnsAsync
                ( new CardanoAPIModel()
                {
                    data=new List<Data>() 
                    {
                        new Data()
                        {
                            attributes=new Attributes()
                            {
                                bic=new List<string>(){ "SBILGB2LXXX" },
                                lei="XKZZ2JZF41MRHTR1V493",
                                entity=new Entity()
                                {
                                    legalAddress=new LegalAddress(){country="GB" },
                                    legalName=new LegalName()
                                    {
                                        name="CITIGROUP GLOBAL MARKETS LIMITED" ,
                                        language="en"
                                    }
                                }
                            }
                        }
                    }
                });
            
            var content = @"transaction_uti,isin,notional,notional_currency,transaction_type,transaction_datetime,rate,lei
1030291281MARKITWIRE0000000000000112874138,EZ9724VTXK48,763000.0,GBP,Sell,2020-11-25T15:06:22Z,0.0070956000,XKZZ2JZF41MRHTR1V493";
            var fileName = "input.csv";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile file = new FormFile(stream, 0, stream.Length, "form", fileName);


            CalculationController calculationController = new CalculationController(
                csvService, cardanoApi.Object,calculationService);

            var result = await calculationController.CaculateAndGetCsv(file);

            result.Content.Should().NotBeEmpty();
            result.Content.Should().NotBeNull();
        }

    }
}
