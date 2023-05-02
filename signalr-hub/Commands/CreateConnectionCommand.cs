using signalr_hub.Enums;

namespace signalr_hub.Commands;

public record CreateConnectionCommand(string? Analyst, string? ConnectionId, GroupEnum Group);