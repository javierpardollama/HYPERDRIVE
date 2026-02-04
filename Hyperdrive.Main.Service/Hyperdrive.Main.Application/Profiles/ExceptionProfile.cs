using Hyperdrive.Main.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Main.Application.Profiles;

/// <summary>
///     Represents a <see cref="ExceptionProfile" /> class
/// </summary>
public static class ExceptionProfile
{
    /// <summary>
    /// Transforms to Code
    /// </summary>
    /// <param name="exception">Injected <see cref="Exception" /></param>
    /// <returns>Instance of <see cref="int" /></returns>
    public static int ToCode(this Exception exception)
    {
        return exception.GetType().Name switch
        {
            nameof(ValidationException) => StatusCodes.Status400BadRequest,
            nameof(ArgumentNullException) => StatusCodes.Status400BadRequest,
            nameof(UnauthorizedAccessException) => StatusCodes.Status401Unauthorized,
            nameof(NullReferenceException) => StatusCodes.Status404NotFound,
            nameof(TimeoutException) => StatusCodes.Status408RequestTimeout,
            nameof(ServiceException) => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}