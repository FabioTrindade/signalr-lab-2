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

    public async Task<GenericCommandResult> Create(CreateCaseAlertCommand command)
    {
        var caseAlert = new CaseAlert(command.Name, command.Description, command.Group);

        var result = await _caseAlertRepository.CreateAsync(caseAlert);

        await NotificationGroup(result);

        return new(result);
    }

    public async Task<GenericCommandResult> Update(UpdateCaseAlertCommand command)
    {
        var caseAlert = await _caseAlertRepository.GetFirstByIdAsync(command.CaseAlertId);

        if(command.IsActive is not null)
            caseAlert.SetActive(command.IsActive.Value);
        
        caseAlert.SetAnalyst(command?.Analyst);
        caseAlert.SetUpdatedAt();

        var result = await _caseAlertRepository.UpdateAsync(caseAlert);

        await NotificationGroup(result);

        return new(result);
    }

    public async Task<GenericCommandResult> GetCaseAlerts(GroupEnum group)
        => new(await _caseAlertRepository.GetAllAsync(w => w.Group == group));

    public async Task<GenericCommandResult> GetGroupCaseAlert()
        => new(GetEnumSelectList<GroupEnum>());

    public static IEnumerable<SelectListItem> GetEnumSelectList<T>()
        => (Enum.GetValues(typeof(T)).Cast<T>()
                .Select(enu =>
                            new SelectListItem()
                            {
                                Text = enu.ToString(),
                                Value = enu.ToString()
                            })).ToList();

    private async Task NotificationGroup(CaseAlert caseAlert)
    {
        await _hubContext.Clients.Group(caseAlert.Group.ToString()).SendAsync("CommunicationReceived", caseAlert);
    }
}