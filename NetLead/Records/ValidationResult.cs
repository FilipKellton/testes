namespace NetLead.Records;
public record ValidationResult<T>(T Item, bool IsValid, string? ErrorMessage);
