using CardanoCalculationBackend.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.Services
{
    public class CalculationService_Unittest
    {
        [Fact]
        public void CalculateTransactioCost_GB_Formula()
        {
            decimal expectedvalue = 5413.9428m;

            CalculationService calculationService = new CalculationService();
            var result = calculationService.CalculateTransactioCost(763000.0m, 0.0070956000m, "GB");

            result.Should().Be(expectedvalue);
        }

        [Fact]
        public void CalculateTransactioCost_NL_Formula()
        {
            decimal expectedvalue = 106768427.92716613112351316308M;

            CalculationService calculationService = new CalculationService();
            var result = calculationService.CalculateTransactioCost(763000.0m, 0.0070956000m, "NL");

            result.Should().Be(expectedvalue);
        }
    }
}
