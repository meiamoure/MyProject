using System.Net;


namespace FlixNet.Infrastructure.Exceptions;

public record ExceptionResponse(HttpStatusCode StatusCode, object Data);
