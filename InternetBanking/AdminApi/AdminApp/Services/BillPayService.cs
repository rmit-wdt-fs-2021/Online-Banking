using AdminApp.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdminApp.Services
{
    public class BillPayService : IBillPayService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<BillPayService> _logger;
        private HttpClient Client => _clientFactory.CreateClient("api");
        private const string Prefix = "api/billpay";

        public BillPayService(IHttpClientFactory clientFactory, ILogger<BillPayService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task BlockBillPayAsync(int billPayId)
        {
            var response = await Client.PutAsync($"{Prefix}/{billPayId}/true", null).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Unable to block bill pay : {billPayId}");
            }
        }

        public async Task UnblockBillPayAsync(int billPayId)
        {
            var response = await Client.PutAsync($"{Prefix}/{billPayId}/false", null).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Unable to unblock bill pay : {billPayId}");
            }
        }
    }
}
