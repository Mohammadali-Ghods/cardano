using CardanoCalculationBackend.Services;
using FluentAssertions;
using Xunit;

namespace UnitTest.Services
{
    public class CsvService_Unittest
    {

        [Fact]
        public void FromCordonaCsv_CurrectValue()
        {
            string sampleinput = "1030291281MARKITWIRE0000000000000112874138,EZ9724VTXK48,763000.0,GBP,Sell,2020-11-25T15:06:22Z,0.0070956000,XKZZ2JZF41MRHTR1V493";
            CsvService csvService = new CsvService();
            var result= csvService.FromCardanoCsv(sampleinput);
            result.lei.Should().Be("XKZZ2JZF41MRHTR1V493");
            result.rate.Should().Be(0.0070956000m);
            result.notional.Should().Be(763000.0m);
            result.notional_currency.Should().Be("GBP");
        }
    }
}
