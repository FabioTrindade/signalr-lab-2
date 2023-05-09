using signalr_hub.Entities;
using signalr_hub.Repositories;

namespace signalr_hub.Services;

public class ConnectionService : IConnectionService
{
    private readonly IConnectionRepository _connectionRepository;

    public ConnectionService(IConnectionRepository connectionRepository)
    {
        _connectionRepository = connectionRepository;
    }

    public async Task Create(Connection connection)
        => await _connectionRepository.Create(connection);
    
    public async Task<IEnumerable<string>> GetAnalyst(string analyst)
        => await _connectionRepository.GetConnectionsByAnalyst(analyst);

    public async Task<IEnumerable<string>> GetGroup(string group)
        => await _connectionRepository.GetConnectionsByGroup(group);

    public async Task<string> GetGroupByAnalyst(string analyst)
        => await _connectionRepository.GetGroupByAnalyst(analyst);

    public async Task Remove(Connection connection)
        => await _connectionRepository.Delete(connection);
}