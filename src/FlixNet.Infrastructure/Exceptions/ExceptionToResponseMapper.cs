using FlixNet.Core.Exceptions;
using System.Net;


namespace FlixNet.Infrastructure.Exceptions;

public class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception)
    {
        return exception switch
        {
            NotFoundException ex => new ExceptionResponse(
                HttpStatusCode.NotFound,
                new
                {
                   ex.Message,
                   statusCode = "404"
                }),
            AlreadyExistsException ex => new ExceptionResponse(
                HttpStatusCode.Conflict,
                new
                {
                    ex.Message,
                    ex.Details,
                    statusCode = "409"
                }),
            ValidationException ex => new ExceptionResponse(
                HttpStatusCode.BadRequest,
                new
                {
                    ex.Message,
                    statusCode = "400",
                    Errors = ex.Failures
                        .Select(e => new
                        {
                            e.PropertyName,
                            e.ErrorMessage
                        })
                        .ToList()
                }),
            RuleValidationException ex => new ExceptionResponse(
                HttpStatusCode.BadRequest,
                new
                {
                    ex.Message,
                    statusCode = "400",
                    Errors = ex.Failures
                }),

            _ => new ExceptionResponse(
                HttpStatusCode.InternalServerError,
                new
                {
                    exception.Message,
                    statusCode = "500",
                    exception.StackTrace
                })
        };
    }
}
