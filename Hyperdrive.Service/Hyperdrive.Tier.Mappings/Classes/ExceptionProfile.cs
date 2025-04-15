using System;

using Hyperdrive.Tier.Exceptions.Exceptions;

using Microsoft.AspNetCore.Http;

namespace Hyperdrive.Tier.Mappings.Classes
{
    /// <summary>
    /// Represents a <see cref="ExceptionProfile"/> class
    /// </summary>
    public static class ExceptionProfile
    {
        /// <summary>
        /// Maps
        /// </summary>
        /// <param name="exception">Injected <see cref="Exception"/></param>
        /// <returns>Instance of <see cref="int"/></returns>
        public static int Map(Exception @exception) 
        {
            return @exception.GetType().Name switch
            {
                nameof(ArgumentNullException) => StatusCodes.Status400BadRequest,
                nameof(UnauthorizedAccessException) => StatusCodes.Status401Unauthorized,
                nameof(TimeoutException) => StatusCodes.Status408RequestTimeout,
                nameof(ServiceException) => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
