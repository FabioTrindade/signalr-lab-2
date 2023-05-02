namespace signalr_client.Commands;

public record UpdateCaseAlertCommand(Guid CaseAlertId, bool? IsActive, string? Analyst);