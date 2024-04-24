using System.Net;

namespace Domain.Common;

public abstract class ExceptionBase(string message, HttpStatusCode httpStatusCode) : Exception(message)
{
    public HttpStatusCode HttpStatusCode { get; } = httpStatusCode;
}