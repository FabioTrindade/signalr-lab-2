using signalr_hub.Enums;

namespace signalr_hub.Commands;

public record GetCaseAlersCommnad(string Analyst, GroupEnum group);