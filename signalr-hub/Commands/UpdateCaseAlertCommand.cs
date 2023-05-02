namespace signalr_hub.Commands;

public record UpdateCaseAlertCommand(Guid CaseAlertId, bool? IsActive, string? Analyst);