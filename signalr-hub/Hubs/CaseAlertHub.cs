using Microsoft.AspNetCore.SignalR;
using signalr_hub.Entities;
using signalr_hub.Enums;
using signalr_hub.Repositories;
using signalr_hub.Services;

namespace signalr_hub.Hubs;

public class CaseAlertHub : Hub
{
    private readonly IConnectionService _connectionService;
    private readonly ICaseAlertRepository _caseAlertRepository;

    public CaseAlertHub(IConnectionService connectionService
        , ICaseAlertRepository caseAlertRepository)
    {
        _connectionService = connectionService;
        _caseAlertRepository = caseAlertRepository;
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

    public async Task AddToRoom(string analyst, string group)
    {
        var connectionId = Context.ConnectionId;

        await ValidateAlreadyInformedGroup(analyst, group, connectionId);

        await _connectionService.Create(new Connection(connectionId, analyst, group));

        await Groups.AddToGroupAsync(connectionId, group);
    }

    public async Task RemoveFromRoom(string analyst, string group)
    {
        await _connectionService.Remove(new Connection(Context.ConnectionId, analyst, group));

        await RemoveAnalystFromRoom(analyst, group);

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
    }

    public async Task CommunicateGroup(CaseAlert caseAlert)
    {
        await Clients.Groups(caseAlert.Group.ToString()).SendAsync("CommunicationReceived", caseAlert);
    }


    private async Task ValidateAlreadyInformedGroup(string analyst, string group, string connectionId)
    {
        var groupActual = await _connectionService.GetGroupByAnalyst(analyst, connectionId);

        if (groupActual is not null && groupActual != group)
            await Groups.RemoveFromGroupAsync(connectionId, group);
    }

    private async Task RemoveAnalystFromRoom(string analyst, string group)
    {
        var analystConnection = await _connectionService.GetAnalyst(analyst);

        if (analystConnection?.Any() == true)
        {
            analystConnection.ToList().ForEach(async connection =>
            {
                await Groups.RemoveFromGroupAsync(connection, group);
            });
        }

        await RemoveGroupAnalystFromRoom(analystConnection, group);
    }

    private async Task RemoveGroupAnalystFromRoom(IEnumerable<string> analystConnection, string group)
    {
        var groupConnection = await _connectionService.GetGroup(group);

        if (groupConnection?.Any() == true)
        {
            var analystGroupConnection = groupConnection.Where(w => analystConnection.Contains(w)).ToList();
            analystGroupConnection.ForEach(async connection =>
            {
                await Groups.RemoveFromGroupAsync(connection, group);
            });

        }
    }
}
