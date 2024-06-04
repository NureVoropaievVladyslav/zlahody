using System.Net;

namespace Domain.Exceptions;

public class BadRequestException(string message) : ExceptionBase(message, HttpStatusCode.BadRequest);