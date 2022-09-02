using CardanoCalculationBackend.Models;
using CardanoCalculationBackend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.Controllers
{
    
    public class CalculationController_Unittest
    {
        [Fact]
        public async Task PostCalculation_WithCurrectData() 
        {
            var csvService = new Mock<CsvService>();
            csvService.Setup(p => p.FromCardanoCsv("1030291281MARKITWIRE0000000000000112874138,EZ9724VTXK48,763000.0,GBP,Sell,2020-11-25T15:06:22Z,0.0070956000,XKZZ2JZF41MRHTR1V493"))
                .Returns(new CsvModel() 
                {
                    transaction_uti = "1030291281MARKITWIRE0000000000000112874138",
                    isin= "EZ9724VTXK48",
                    notional= 763000.0m,
                    notional_currency= "GBP",
                    lei= "XKZZ2JZF41MRHTR1V493"
                });


        }
       
    }
}
