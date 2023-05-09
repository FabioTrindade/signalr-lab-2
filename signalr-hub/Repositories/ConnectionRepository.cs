using signalr_hub.Entities;
using signalr_hub.Providers;

namespace signalr_hub.Repositories;

public class ConnectionRepository : IConnectionRepository
{
    private readonly ICacheProvider _cacheProvider;

    public ConnectionRepository(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    public async Task Create(Connection connection)
        => await ConnectionCreationManagement(connection);

    public async Task<IEnumerable<string>> GetConnectionsByAnalyst(string analyst)
        => await _cacheProvider.TryGetValueAsync<List<string>>(analyst);

    public async Task<IEnumerable<string>> GetConnectionsByGroup(string group)
        => await _cacheProvider.TryGetValueAsync<List<string>>(group);

    public async Task<string> GetGroupByAnalyst(string analyst)
        => await _cacheProvider.TryGetValueAsync(analyst);

    public async Task Delete(Connection connection)
        => await ManagementConnectionDelete(connection);


    private async Task ConnectionCreationManagement(Connection connection)
    {
        await ManagementConnectionCreationAnalystGroup(connection);
        await ManagementConnectionCreationGroup(connection);
    }

    private async Task ManagementConnectionCreationAnalystGroup(Connection connection)
    {
        var analystGroupConnection = await _cacheProvider.TryGetValueAsync(connection.Analyst);

        if (string.IsNullOrWhiteSpace(analystGroupConnection))
            await _cacheProvider.TrySetValueAsync(connection.Analyst, connection.Group);
        else
            await ManagementConnectionAnalystOtherGroup(connection);
    }

    private async Task ManagementConnectionAnalystOtherGroup(Connection connection)
    {
        var group = await _cacheProvider.TryGetValueAsync(connection.Analyst);

        if (group != connection.Group)
        {
            List<string> groupConnection = await _cacheProvider.TryGetValueAsync<List<string>>(group);

            if (groupConnection.Any())
            {
                groupConnection.RemoveAll(r => r == connection.ConnectionId);
                await _cacheProvider.TrySetValueAsync(group, groupConnection);
            }

            await _cacheProvider.TrySetValueAsync(connection.Analyst, connection.Group);
        }
    }

    private async Task ManagementConnectionCreationGroup(Connection connection)
    {
        if (string.IsNullOrWhiteSpace(connection.Group))
            return;

        List<string> groupConnection = await _cacheProvider.TryGetValueAsync<List<string>>(connection.Group);

        groupConnection?.Add(connection?.ConnectionId);

        groupConnection ??= new List<string> { connection?.ConnectionId };

        var groupConnectionGroup = groupConnection.GroupBy(gb => gb).Select(s => s.Key).ToList();

        await _cacheProvider.TrySetValueAsync(connection.Group, groupConnectionGroup);
    }

    private async Task ManagementConnectionDelete(Connection connection)
    {
        await ManagementConnectionDeleteAnalyst(connection);
        await ManagementConnectionDeleteGroup(connection);
    }

    private async Task ManagementConnectionDeleteAnalyst(Connection connection)
    {
        var analystConnection = await _cacheProvider.TryGetValueAsync<List<string>>(connection.Analyst);

        if (analystConnection is not null)
            await _cacheProvider.TryDeleteValueAsync(connection.Analyst);

        await _cacheProvider.TryDeleteValueAsync(connection.ConnectionId);
    }

    private async Task ManagementConnectionDeleteGroup(Connection connection)
    {
        List<string> analystConnection = await _cacheProvider.TryGetValueAsync<List<string>>(connection.Analyst);
        List<string> groupConnection = await _cacheProvider.TryGetValueAsync<List<string>>(connection.Group);

        if (groupConnection.Any())
        {
            var managerConnections = groupConnection.Except(analystConnection);
            await _cacheProvider.TrySetValueAsync(connection.Group, managerConnections);
        }
    }
}