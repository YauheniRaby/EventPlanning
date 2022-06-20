using EventPlanning.Bl.Configuration;
using EventPlanning.Bl.Services.Abstract;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventPlanning.Bl.Services
{
    public class SmsService : ISmsService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<SmsApiConfiguration> _optionsMonitor;

        public SmsService(HttpClient httpClient, IOptionsMonitor<SmsApiConfiguration> optionsMonitor)
        {
            _httpClient = httpClient;
            _optionsMonitor = optionsMonitor;
        }

        public Task SendCodeAsync(string phone, int verifiedCode)
        {
            var url = string.Format(
                _optionsMonitor.CurrentValue.Adress,
                verifiedCode,
                phone,
                _optionsMonitor.CurrentValue.Key);
            return _httpClient.GetAsync(url);
        }        
    }
}
