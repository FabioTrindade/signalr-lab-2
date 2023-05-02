using signalr_hub.Commands;
using signalr_hub.Entities;
using signalr_hub.Enums;

namespace signalr_hub.Repositories;

public class CaseAlertRepository : ICaseAlertRepository
{
    private static List<CaseAlert> _caseAlerts = new();

    public async Task Create(CaseAlert caseAlert)
        => _caseAlerts.Add(caseAlert);

    public async Task<IEnumerable<CaseAlert>> GetAll()
        => _caseAlerts;

    public async Task<IEnumerable<CaseAlert>> GetCaseAlertByGroup(GroupEnum group)
        => _caseAlerts.Where(w => w.Group == group).ToList();

    public async Task<CaseAlert> GetByCaseAlertId(Guid id)
        => _caseAlerts.FirstOrDefault(w => w.Id == id);

    public async Task Update(UpdateCaseAlertCommand command)
        => _caseAlerts
            .Where(i => i.Id == command.CaseAlertId)
            .ToList()
            .ForEach(i =>
            {
                i.SetActive(command.IsActive ?? i.Active);
                i.SetAnalyst(command.Analyst ?? i.Analyst);
                i.SetUpdatedAt();
            });
}