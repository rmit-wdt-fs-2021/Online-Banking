using AdminApp.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdminApp.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<UserAccountService> _logger;

        private HttpClient Client => _clientFactory.CreateClient("api");

        public UserAccountService(IHttpClientFactory clientFactory, ILogger<UserAccountService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }


    }
}
