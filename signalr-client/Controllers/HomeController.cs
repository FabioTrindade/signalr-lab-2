using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using signalr_client.Commands;
using signalr_client.Configurations;
using signalr_client.Models;
using signalr_client.Services;
using System.Diagnostics;

namespace signalr_client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICaseAlertService _caseAlertService;
        private readonly IOptions<HttpClientSetttings> _config;

        public HomeController(ILogger<HomeController> logger
            , ICaseAlertService caseAlertService
            , IOptions<HttpClientSetttings> config)
        {
            _logger = logger;
            _caseAlertService = caseAlertService;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        public async Task<IActionResult> CaseAlert()
        {
            return View();
        }

        [HttpGet("get-case-alerts")]
        public async Task<IActionResult> GetCaseAlertsAsync(GetCaseAlertsCommand command)
        {
            var result = await _caseAlertService.GetCaseAlertsAsync(command);
            return Json(result.Data);
        }

        [HttpGet("get-group-alerts")]
        public async Task<IActionResult> GetGroupAlertsAsync()
        {
            var result = await _caseAlertService.GetGroupAlertAsync();
            return Json(result.Data);
        }

        [HttpGet("url-base-hub")]
        public async Task<IActionResult> GetUrlBaseHub()
        {
            return Json(_config.Value.BaseUrl);
        }

        [HttpPatch("update-analyst-case-alert")]
        public async Task<IActionResult> UpdateAnalystCaseAlert([FromBody] UpdateCaseAlertCommand command)
        {
            var result = await _caseAlertService.PatchCaseAlertAsync(command);
            return Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}