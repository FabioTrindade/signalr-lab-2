using signalr_hub.Commands;
using signalr_hub.Enums;

namespace signalr_hub.Services
{
    public interface ICaseAlertService
    {
        Task Create(CreateCaseAlertCommand command);

        void Update(UpdateCaseAlertCommand command);

        GenericCommandResult GetCaseAlerts(GroupEnum group);

        GenericCommandResult GetGroupCaseAlert();
    }
}