using Microsoft.Extensions.Options;
using signalr_client.Commands;
using signalr_client.Configurations;
using signalr_client.Controllers;
using signalr_client.Helpers;
using signalr_client.Models;

namespace signalr_client.Services
{
    public class CaseAlertService : ICaseAlertService
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IOptions<HttpClientSetttings> _config;

        public CaseAlertService(ILogger<HomeController> logger
            , IOptions<HttpClientSetttings> config
            , IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClient = httpClientFactory.CreateClient("DefaultHttpClient");
            _httpClient.BaseAddress = new Uri(_config.Value.BaseUrl);
        }

        public async Task<GenericCommandResult<List<GroupAlertModel>>> GetGroupAlertAsync()
        {
            _logger.LogInformation($"Stating method {nameof(GetGroupAlertAsync)}.");

            var response = await _httpClient.GetAsync(_config.Value.GroupAlert);
            var result = await HttpClientHelper.DeserializeObjectResponse<List<GroupAlertModel>>(response);

            _logger.LogInformation($"Finish method {nameof(GetGroupAlertAsync)}.");

            return new GenericCommandResult<List<GroupAlertModel>>(result);
        }

        public async Task<GenericCommandResult<List<CaseAlertModel>>> GetCaseAlertsAsync(GetCaseAlertsCommand command)
        {
            _logger.LogInformation($"Stating method {nameof(GetCaseAlertsAsync)}.");

            if (command.Group is null || command.Analyst is null)
                return new GenericCommandResult<List<CaseAlertModel>>(new List<CaseAlertModel>());

            var content = HttpClientHelper.GetContent(command);
            var response = await _httpClient.GetAsync(string.Format(_config.Value.CaseAlert, content));
            var result = await HttpClientHelper.DeserializeObjectResponse<List<CaseAlertModel>>(response);

            _logger.LogInformation($"Finish method {nameof(GetCaseAlertsAsync)}.");

            return new GenericCommandResult<List<CaseAlertModel>>(result);
        }

        public async Task<GenericCommandResult<CaseAlertModel>> PatchCaseAlertAsync(UpdateCaseAlertCommand command)
        {
            _logger.LogInformation($"Stating method {nameof(PatchCaseAlertAsync)}.");

            var content = HttpClientHelper.GetContent(command);
            var response = await _httpClient.GetAsync(string.Format(_config.Value.UpdateCaseAlert, content));
            var result = await HttpClientHelper.DeserializeObjectResponse<CaseAlertModel>(response);

            _logger.LogInformation($"Finish method {nameof(PatchCaseAlertAsync)}.");

            return new GenericCommandResult<CaseAlertModel>(result);
        }
    }
}