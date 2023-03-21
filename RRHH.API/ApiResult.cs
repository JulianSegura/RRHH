namespace RRHH.API;

public record ApiResult(bool isSuccessful, object? Data = null, List<string>? Errors = null) { }