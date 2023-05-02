using signalr_client.Commands;
using signalr_client.Models;

namespace signalr_client.Services;

public interface ICaseAlertService
{
    Task<GenericCommandResult<List<GroupAlertModel>>> GetGroupAlertAsync();

    Task<GenericCommandResult<List<CaseAlertModel>>> GetCaseAlertsAsync(GetCaseAlertsCommand command);

    Task<GenericCommandResult<CaseAlertModel>> PatchCaseAlertAsync(UpdateCaseAlertCommand command);
}