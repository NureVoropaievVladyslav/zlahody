using System.Net;

namespace Domain.Exceptions;

public class ForbiddenException(string message) : ExceptionBase(message, HttpStatusCode.Forbidden);