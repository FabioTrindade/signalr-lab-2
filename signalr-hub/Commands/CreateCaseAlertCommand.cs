using signalr_hub.Enums;

namespace signalr_hub.Commands;

public record CreateCaseAlertCommand(string? Name, string? Description, GroupEnum Group);