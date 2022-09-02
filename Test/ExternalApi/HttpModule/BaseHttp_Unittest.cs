using ExternalApi.HttpModule;
using ExternalApi.Models;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Test.ExternalApi.HttpModule
{
    public class BaseHttp_Unittest
    {
        [Fact]
        public async Task CardanoApi_CheckIntegrationWithBaseHttp()
        {
            var result=
                await BaseHttp.Get<CardanoAPIModel>(null, "https://api.gleif.org/api/v1/",
                "lei-records?filter[lei]=XKZZ2JZF41MRHTR1V493"); 

            result.data[0].attributes.bic[0].Should().Be("SBILGB2LXXX");
        }
    }
}
