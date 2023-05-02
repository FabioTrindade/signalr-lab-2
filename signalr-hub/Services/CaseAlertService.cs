using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using signalr_hub.Commands;
using signalr_hub.Entities;
using signalr_hub.Enums;
using signalr_hub.Hubs;
using signalr_hub.Repositories;

namespace signalr_hub.Services;

public class CaseAlertService : ICaseAlertService
{
    private readonly ICaseAlertRepository _caseAlertRepository;
    private readonly IHubContext<CaseAlertHub> _hubContext;

    public CaseAlertService(ICaseAlertRepository caseAlertRepository
        , IHubContext<CaseAlertHub> hubContext)
    {
        _caseAlertRepository = caseAlertRepository;
        _hubContext = hubContext;
    }

    public async Task Create(CreateCaseAlertCommand command)
    {
        await _caseAlertRepository.Create(new CaseAlert(command.Name, command.Description, command.Group));

        await NotificationGroup(command.Group);
    }

    public async void Update(UpdateCaseAlertCommand command)
    {
        var caseAlertUpdated = await _caseAlertRepository.GetByCaseAlertId(command.CaseAlertId);
        
        await _caseAlertRepository.Update(command);

        await NotificationGroup(caseAlertUpdated.Group);
    }

    public GenericCommandResult GetCaseAlerts(GroupEnum group)
        => new(_caseAlertRepository.GetCaseAlertByGroup(group));

    public GenericCommandResult GetGroupCaseAlert()
        => new(GetEnumSelectList<GroupEnum>());

    public static IEnumerable<SelectListItem> GetEnumSelectList<T>()
        => (Enum.GetValues(typeof(T)).Cast<T>()
                .Select(enu =>
                            new SelectListItem()
                            {
                                Text = enu.ToString(),
                                Value = enu.ToString()
                            })).ToList();

    private async Task NotificationGroup(GroupEnum group)
    {
        var result = await _caseAlertRepository.GetCaseAlertByGroup(group);

        await _hubContext.Clients.Groups(group.ToString()).SendAsync("CommunicationReceived", result);
    }
}