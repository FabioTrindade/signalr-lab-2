using Microsoft.EntityFrameworkCore;
using signalr_hub.Entities;

namespace signalr_hub.Context;

public class SignalRDataContext : DbContext
{
    public SignalRDataContext(DbContextOptions<SignalRDataContext> options) : base(options)
    {  
    }

    public DbSet<CaseAlert> CaseAlerts { get; set; }
}