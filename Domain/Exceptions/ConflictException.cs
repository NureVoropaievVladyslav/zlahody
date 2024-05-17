namespace Domain.Exceptions;

public class ConflictException(string message) : ExceptionBase(message,System.Net.HttpStatusCode.Conflict);