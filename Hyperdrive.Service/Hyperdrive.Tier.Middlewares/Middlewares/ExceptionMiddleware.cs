using Hyperdrive.Tier.Exceptions.Exceptions;
using Hyperdrive.Tier.ViewModels.Classes.Views;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hyperdrive.Tier.Middlewares.Middlewares
{
    /// <summary>
    /// Represents a <see cref="ExceptionMiddleware"/> class
    /// </summary>  
    /// <param name="request">Injected <see cref="RequestDelegate"/></param>
    public class ExceptionMiddleware(RequestDelegate @request)
    {
        /// <summary>
        /// Invoques Asynchronously
        /// </summary>
        /// <param name="context">Injected <see cref="HttpContext"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task InvokeAsync(HttpContext @context)
        {
            try
            {
                await @request(@context);
            }
            catch (ServiceException @exception)
            {
                await HandleExceptionAsync(@context, @exception);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Handles Exception Asynchronously
        /// </summary>
        /// <param name="context">Injected <see cref="HttpContext"/></param>
        /// <param name="exception">Injected <see cref="ServiceException"/></param>
        /// <returns>Instance of <see cref="ViewServiceException"/></returns>
        private static Task HandleExceptionAsync(
            HttpContext @context,
            ServiceException @exception)
        {
            @context.Response.ContentType = "application/json";
            @context.Response.StatusCode = (int)HttpStatusCode.Conflict;

            ViewServiceException @viewException = new()
            {
                Message = @exception.Message
            };

            return @context.Response.WriteAsJsonAsync(@viewException, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}