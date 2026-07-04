// -----------------------------------------------------------------------
// <copyright file="ExceptionToResponseMapper.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Infrastructure.Exceptions;

/// <summary>
/// Represents exception to response mapper.
/// </summary>
internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    private static readonly ConcurrentDictionary<Type, string> Codes = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionToResponseMapper"/> class.
    /// </summary>
    public ExceptionToResponseMapper()
    {
    }

    /// <summary>
    /// Maps the exception to response.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>Returns the exception response mapped with defined type.</returns>
    public ExceptionResponse Map(Exception exception)
    {
        return exception switch
        {
            DomainException ex => new ExceptionResponse(
                new { code = GetCode(ex), reason = ex.Message },
                HttpStatusCode.BadRequest),
            AppException ex => new ExceptionResponse(
                new { code = GetCode(ex), reason = ex.Message },
                HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(
                new { code = "error", reason = "There was an error." },
                HttpStatusCode.InternalServerError)
        };
    }

    private static string GetCode(Exception exception)
    {
        var type = exception.GetType();
        if (Codes.TryGetValue(type, out var code))
        {
            return code;
        }

        var exceptionCode = exception switch
        {
            DomainException domainException when !string.IsNullOrWhiteSpace(domainException.Code) => domainException.Code,
            AppException appException when !string.IsNullOrWhiteSpace(appException.Code) => appException.Code,
            _ => exception.GetType().Name.Underscore().Replace("_exception", string.Empty)
        };

        Codes.TryAdd(type, exceptionCode);

        return exceptionCode;
    }
}
