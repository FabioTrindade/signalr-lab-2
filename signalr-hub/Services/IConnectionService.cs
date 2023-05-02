using signalr_hub.Entities;
using System.Text.RegularExpressions;

namespace signalr_hub.Services
{
    public interface IConnectionService
    {
        Task Create(Connection connection);

        Task Remove(Connection connectionId);

        Task<IEnumerable<string>> GetAnalyst(string analyst);

        Task<IEnumerable<string>> GetGroup(string group);

        Task<string> GetGroupByAnalyst(string analyst, string connectionId);
    }
}