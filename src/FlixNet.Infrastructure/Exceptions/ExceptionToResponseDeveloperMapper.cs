using System.Net;
using FlixNet.Core.Exceptions;

namespace FlixNet.Infrastructure.Exceptions;

public class ExceptionToResponseDeveloperMapper : IExceptionToResponseDeveloperMapper
{
    public ExceptionResponse Map(Exception exception)
    {
        return exception switch
        {
            NotFoundException => new ExceptionResponse(
                HttpStatusCode.NotFound,
                new
                {
                    exception.Message,
                    exception.StackTrace
                }),

            AlreadyExistsException ex => new ExceptionResponse(
                HttpStatusCode.Conflict,
                new
                {
                    ex.Message,
                    ex.Details,
                    exception.StackTrace
                }),

            ValidationException ex => new ExceptionResponse(
                HttpStatusCode.BadRequest,
                new
                {
                    ex.Message,
                    Errors = ex.Failures.Select(f => new
                    {
                        f.PropertyName,
                        f.ErrorMessage
                    }),
                    exception.StackTrace
                }),

            RuleValidationException ex => new ExceptionResponse(
                HttpStatusCode.BadRequest,
                new
                {
                    ex.Message,
                    Errors = ex.Failures,
                    exception.StackTrace
                }),

            UnauthorizedAccessException => new ExceptionResponse(
                HttpStatusCode.Unauthorized,
                new
                {
                    exception.Message,
                    exception.StackTrace
                }),

            _ => new ExceptionResponse(
                HttpStatusCode.InternalServerError,
                new
                {
                    exception.Message,
                    exception.StackTrace
                })
        };
    }
}

