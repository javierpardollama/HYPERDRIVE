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
    public class ExceptionMiddleware
    {
        /// <summary>
        /// Instance of <see cref="RequestDelegate"/>
        /// </summary>
        private readonly RequestDelegate Request;

        /// <summary>
        /// Initializes a new Instance of <see cref="ExceptionMiddleware"/>
        /// </summary>
        /// <param name="request">Injected <see cref="RequestDelegate"/></param>
        public ExceptionMiddleware(RequestDelegate @request) => Request = @request;

        /// <summary>
        /// Invoques Asynchronously
        /// </summary>
        /// <param name="context">Injected <see cref="HttpContext"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task InvokeAsync(HttpContext @context)
        {
            try
            {
                await Request(@context);
            }
            catch (Exception @ex)
            {
                await HandleExceptionAsync(@context, @ex);
            }
        }

        /// <summary>
        /// Handles Expception Asynchronously
        /// </summary>
        /// <param name="context">Injected <see cref="HttpContext"/></param>
        /// <param name="exception">Injected <see cref="Exception"/></param>
        /// <returns>Instance of <see cref="ViewException"/></returns>
        private static Task HandleExceptionAsync(
            HttpContext @context,
            Exception @exception)
        {
            @context.Response.ContentType = "application/json";
            @context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ViewException @viewException = new()
            {
                StatusCode = @context.Response.StatusCode,
                Message = @exception.Message
            };

            return @context.Response.WriteAsJsonAsync<ViewException>(@viewException, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}