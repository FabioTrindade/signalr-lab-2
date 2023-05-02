using signalr_hub.Commands;
using signalr_hub.Entities;
using signalr_hub.Enums;

namespace signalr_hub.Repositories
{
    public interface ICaseAlertRepository
    {
        Task Create(CaseAlert command);

        Task<IEnumerable<CaseAlert>> GetAll();

        Task Update(UpdateCaseAlertCommand command);

        Task<CaseAlert> GetByCaseAlertId(Guid id);

        Task<IEnumerable<CaseAlert>> GetCaseAlertByGroup(GroupEnum group);
    }
}