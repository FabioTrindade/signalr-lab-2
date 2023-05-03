using signalr_hub.Commands;
using signalr_hub.Enums;

namespace signalr_hub.Services
{
    public interface ICaseAlertService
    {
        Task Create(CreateCaseAlertCommand command);

        Task<GenericCommandResult> Update(UpdateCaseAlertCommand command);

        Task<GenericCommandResult> GetCaseAlerts(GroupEnum group);

        Task<GenericCommandResult> GetGroupCaseAlert();
    }
}