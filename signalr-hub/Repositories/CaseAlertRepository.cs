using signalr_hub.Context;
using signalr_hub.Entities;

namespace signalr_hub.Repositories;

public class CaseAlertRepository : BaseEntityRepository<CaseAlert>, ICaseAlertRepository
{
    public CaseAlertRepository(SignalRDataContext context) : base(context)
    {
    }
}