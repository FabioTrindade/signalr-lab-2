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
        public async Task<ActionResult<GenericCommandResult>> PostCaseAlertGenerate([FromBody] int numberCaseAlert)
        {
            var total = Enum.GetNames(typeof(GroupEnum)).Length;

            for (int i = 0; i < numberCaseAlert; i++)
            {
                var mod = i % total;

                _caseAlertService.Create(new CreateCaseAlertCommand($"Alert {(GroupEnum)mod} {i}", $"Description {(GroupEnum)mod} {i}", (GroupEnum)mod));

                Thread.Sleep(1000);
            }

            return new GenericCommandResult("Case alert sent successfully to all users!");
        }

        [HttpPost("case-alerts")]
        public async Task<ActionResult<GenericCommandResult>> PostCaseAlert([FromBody] CreateCaseAlertCommand command)
            => await _caseAlertService.Create(command);

        [HttpGet("case-alerts/{group}")]
        public async Task<ActionResult<GenericCommandResult>> GetCaseAlerts([FromRoute] GroupEnum group)
            => await _caseAlertService.GetCaseAlerts(group);
        
        [HttpPatch("case-alerts")]
        public async Task<ActionResult<GenericCommandResult>> Patch([FromBody] UpdateCaseAlertCommand command)
            => await _caseAlertService.Update(command);

        [HttpGet("group-alert")]
        public async Task<ActionResult<GenericCommandResult>> GetGroupCaseAlert()
            => await _caseAlertService.GetGroupCaseAlert();
        
    }
}