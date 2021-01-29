using AdminApp.Interfaces;
using Microsoft.Extensions.Logging;
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

        public async Task LockAccountAsync(int id)
        {
            var response = await Client.PutAsync($"api/useraccount/{id}", null);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Unable to lock {id}");
            }

        }
    }
}
