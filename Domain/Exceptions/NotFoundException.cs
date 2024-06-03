namespace Domain.Exceptions;

public class NotFoundException(string message) : ExceptionBase(message, System.Net.HttpStatusCode.NotFound);