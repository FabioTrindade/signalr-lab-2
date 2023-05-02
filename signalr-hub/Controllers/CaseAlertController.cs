using Microsoft.AspNetCore.Mvc;
using signalr_hub.Commands;
using signalr_hub.Enums;
using signalr_hub.Services;

namespace signalr_hub.Controllers
{
    [Route("api/v1")]
    public class CaseAlertController : Controller
    {
        private readonly ICaseAlertService _caseAlertService;

        public CaseAlertController(ICaseAlertService caseAlertService)
        {
            _caseAlertService = caseAlertService;
        }

        [HttpPost("case-alerts-generate")]
        public string PostCaseAlertGenerate([FromBody] int numberCaseAlert)
        {
            for (int i = 0; i < numberCaseAlert; i++)
            {
                var mod = i % 4;

                _caseAlertService.Create(new CreateCaseAlertCommand($"Alert {((GroupEnum)mod).ToString()} {i}", $"Description {((GroupEnum)mod).ToString()} {i}", (GroupEnum)mod));

                Thread.Sleep(1000);
            }

            return "Case alert sent successfully to all users!";
        }

        [HttpPost("case-alerts")]
        public string PostCaseAlert([FromBody] CreateCaseAlertCommand command)
        {
            _caseAlertService.Create(command);
            return "Case alert sent successfully to all users!";
        }

        [HttpGet("case-alerts/{group}")]
        public GenericCommandResult GetCaseAlerts([FromRoute] GroupEnum group)
            => _caseAlertService.GetCaseAlerts(group);
        
        [HttpPatch("case-alerts")]
        public string Patch([FromBody] UpdateCaseAlertCommand command)
        {
            _caseAlertService.Update(command);
            return "Case alert sent successfully to all users!";
        }

        [HttpGet("group-alert")]
        public GenericCommandResult GetGroupCaseAlert()
            => _caseAlertService.GetGroupCaseAlert();
        
    }
}