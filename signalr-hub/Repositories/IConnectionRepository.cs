using signalr_hub.Entities;

namespace signalr_hub.Repositories
{
    public interface IConnectionRepository
    {
        Task Create(Connection connection);

        Task Delete(Connection connection);

        Task<IEnumerable<string>> GetConnectionsByAnalyst(string analyst);

        Task<IEnumerable<string>> GetConnectionsByGroup(string group);

        Task<string> GetGroupByAnalyst(string analyst, string connectionId);
    }
}