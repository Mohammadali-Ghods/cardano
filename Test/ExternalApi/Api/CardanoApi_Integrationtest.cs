using ExternalApi.Api;
using ExternalApi.ConfigurationModel;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.ExternalApi.Api
{
    public class CardanoApi_Integrationtest
    {
        [Fact]
        public async Task CardanoApi_CheckIntegrationWithBaseHttp()
        {
            var optionMonitor = new Mock<IOptionsMonitor<ExretnalApiModel>>();
            optionMonitor.Setup(p => p.CurrentValue).Returns(new ExretnalApiModel()
            { CardanoAPIUrl = "https://api.gleif.org/api/v1/" });

            CardanoApi cardanoApi = new CardanoApi(optionMonitor.Object);
            var result = await cardanoApi.GetRecord("XKZZ2JZF41MRHTR1V493");
            result.data[0].attributes.bic[0].Should().Be("SBILGB2LXXX");
        }
    }
}
