﻿using AdminApp.AutoGeneratedModel;
using AdminApp.Controllers;
using AdminApp.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdminApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<TransactionService> _logger;
        private HttpClient Client => _clientFactory.CreateClient("api");

        public TransactionService(IHttpClientFactory clientFactory, ILogger<TransactionService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            var transactionResponse = await Client.GetAsync($"api/transaction");

            if (!transactionResponse.IsSuccessStatusCode)
            {
                _logger.LogError($"Unable to find any transactions.");
            }

            var result = await transactionResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var transactions = JsonConvert.DeserializeObject<List<Transaction>>(result);
            return transactions;
        }

        public async Task<List<Transaction>> GetTransactionsAsync(int accountNumber, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var id = accountNumber.ToString();
            HttpResponseMessage transactionResponse;
            const string baseUri = "api/transaction";
            if (fromDate.HasValue && toDate.HasValue)
            {
                transactionResponse = await Client.GetAsync($"{baseUri}/{id}/{ToSqlFormat(fromDate)}/{ToSqlFormat(toDate)}");
            }else if(fromDate.HasValue && toDate == null)
            {
                transactionResponse = await Client.GetAsync($"{baseUri}/{id}/{ToSqlFormat(fromDate)}");
            }
            else
            {
                transactionResponse = await Client.GetAsync($"{baseUri}/{id}");

            }

            if (!transactionResponse.IsSuccessStatusCode)
            {
                _logger.LogError($"Unable to find transactions for account {accountNumber}");
            }

            var result = await transactionResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var transactions = JsonConvert.DeserializeObject<List<Transaction>>(result);
            return transactions;
        }

        private static string ToSqlFormat(DateTime? dateTime)
        {
            var sqlFormattedDate = dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return sqlFormattedDate;
        }

    }
}
