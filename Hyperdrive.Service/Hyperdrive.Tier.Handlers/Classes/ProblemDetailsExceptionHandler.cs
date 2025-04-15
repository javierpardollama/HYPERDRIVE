﻿using Hyperdrive.Tier.Mappings.Classes;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Tier.Handlers.Classes
{
    /// <summary>
    /// Represents a <see cref="ProblemDetailsExceptionHandler"/> class. Implements <see cref="IExceptionHandler"/>
    /// </summary>    
    /// <param name="problemDetailsService">Injected <see cref="IProblemDetailsService"/></param>
    public class ProblemDetailsExceptionHandler(IProblemDetailsService @problemDetailsService) : IExceptionHandler
    {
        /// <summary>
        /// Handles Exception Async
        /// </summary>
        /// <param name="httpContext">Injected <see cref="HttpContext"/></param>
        /// <param name="exception">Injected <see cref="Exception"/></param>
        /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
        /// <returns>Instance of <see cref="ValueTask{bool}"/></returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = ExceptionProfile.Map(exception);

            return await @problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails =
                {
                    Title = "https://www.iana.org/assignments/http-status-codes/http-status-codes.xhtml",
                    Detail = exception.Message,
                    Type = exception.GetType().Name,
                },
                Exception = exception
            });
        }
    }
}
