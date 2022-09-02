using ExternalApi.ConfigurationModel;
using ExternalApi.HttpModule;
using ExternalApi.Interface;
using ExternalApi.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExternalApi.Api
{
    public class CardanoApi : ICardanoAPI
    {
        #region Fields
        private readonly ExretnalApiModel _exretnalApiModel;
        #endregion

        #region Ctor
        public CardanoApi(IOptionsMonitor<ExretnalApiModel> optionsMonitor)
        {
            _exretnalApiModel = optionsMonitor.CurrentValue;
        }
        #endregion

        public async Task<CardanoAPIModel> GetRecord(string lei)
        {
            var cardanoApiModel = await BaseHttp.Get<CardanoAPIModel>
                (
              null
                , _exretnalApiModel.CardanoAPIUrl, "lei-records?filter[lei]=" + lei);

            return cardanoApiModel;
        }
    }
}
